﻿using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using ModControlPanel.Internals.ControlPanel;
using Messages.Logic;
using Messages.Definitions;


namespace Messages.UI {
	partial class UIMessagesTab : UIControlPanelTab {
		public bool OpenNextUnreadMessage() {
			ISet<string> unreadMsgIds = ModContent.GetInstance<MessageManager>()
				.GetUnreadMessages();

			if( unreadMsgIds.Count() == 0 ) {
				return false;
			}

			this.OpenMessage( unreadMsgIds.First(), false );
			return true;
		}

		public void SetAllMessagesRead() {
			ISet<string> unreadMsgIds = ModContent.GetInstance<MessageManager>().GetUnreadMessages();
			var mycustomplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );

			foreach( string msgId in unreadMsgIds ) {
				mycustomplayer.SetReadMessage( msgId );
			}
		}

		public void SetCurrentMessageUnread() {
			string recentMsgId = this.RecentMessageID;
			if( recentMsgId == null ) {
				return;
			}

			var mycustomplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );
			mycustomplayer.UnsetReadMessage( recentMsgId );
		}


		////

		public void OpenMessage( string id, bool exclusively ) {
			if( exclusively ) {
				this.CloseAllMessages();
			}

			//

			this.MessageElems[ id ].DisplayMessageBody( true );

			//

			var hierarchy = new List<UIMessage>();

			for( Message msg = this.MessageElems[id].Message.Parent;
					 msg != null;
					 msg = msg.Parent ) {
				hierarchy.Insert( 0, this.MessageElems[msg.ID] );
			}

			foreach( UIMessage msg in hierarchy ) {
				if( !msg.IsTreeExpanded ) {
					msg.ExpandTree( false );
				}
			}
		}

		public void CloseAllMessages() {
			foreach( var msgElem in this.MessageElems.Values ) {
				if( msgElem.IsTreeExpanded ) {
					msgElem.CollapseTree( false );
				}
			}
		}
	}
}

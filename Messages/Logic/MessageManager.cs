﻿using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Theme;
using ModControlPanel;
using ModControlPanel.Services.UI.ControlPanel;
using Messages.Definitions;
using Messages.UI;


namespace Messages.Logic {
	partial class MessageManager : ILoadable {
		internal UIMessagesTab MessagesTabUI;


		////////////////

		public ConcurrentDictionary<string, Message> MessagesByID { get; } = new ConcurrentDictionary<string, Message>();



		////////////////

		void ILoadable.OnModsLoad() {
			if( !Main.dedServ && Main.netMode != NetmodeID.Server ) {
				var ctrlPanelMod = (ModControlPanelMod)ModLoader.GetMod( "ModControlPanel" );

				ctrlPanelMod.OnControlPanelInitialize += () => {
					this.MessagesTabUI = new UIMessagesTab( UITheme.Vanilla );

					// Add tab
					ControlPanelTabs.AddTab( MessagesMod.ControlPanelName, this.MessagesTabUI );
				};
			}
		}

		void ILoadable.OnPostModsLoad() { }

		void ILoadable.OnModsUnload() { }


		////////////////

		public ISet<string> GetUnreadMessages() {
			var mycustomplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );
			ISet<string> readMsgIds = mycustomplayer.GetReadMessageIdsForCurrentWorld();

			var unreadMsgIds = new HashSet<string>(
				this.MessagesByID.Keys
					.Where( id => !readMsgIds.Contains( id ) )
			);

			return unreadMsgIds;
		}


		////////////////

		public Message AddMessage(
					string title,
					string description,
					out string result,
					string id = null,
					int weight = 0,
					Message parent = null ) {
			if( id != null ) {
				if( this.MessagesByID.ContainsKey(id) ) {
					result = "Message already exists by ID.";
					return null;
				}
			} else if( this.MessagesByID.ContainsKey(title) ) {
				result = "Message already exists by ID (title).";
				return null;
			}

			//

			var msg = new Message( title, description, id, weight );

			if( parent != null ) {
				parent.AddChild( msg );
			}

			this.MessagesByID[ msg.ID ] = msg;

			//

			this.MessagesTabUI.AddMessageAsElementInListIf( msg, parent );

			result = "Success.";
			return msg;
		}

		public bool RemoveMessage( Message message, bool forceUnread = false ) {
			bool isRemoved = this.MessagesByID.TryRemove( message.ID, out Message msg );

			if( forceUnread && isRemoved ) {
				var myplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.LocalPlayer.whoAmI );
				myplayer.UnsetReadMessage( msg.ID );
			}

			this.MessagesTabUI.RemoveMessageElementFromList( message );

			return isRemoved;
		}

		public void ClearAllMessages( bool forceUnread = false ) {
			if( forceUnread ) {
				var myplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.LocalPlayer.whoAmI );

				foreach( Message msg in this.MessagesByID.Values ) {
					myplayer.UnsetReadMessage( msg.ID );
				}
			}

			this.MessagesByID.Clear();
		}
	}
}

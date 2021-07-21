using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using ModLibsCore.Libraries.Debug;
using ModControlPanel.Internals.ControlPanel;


namespace Messages.UI {
	partial class UIMessagesTab : UIControlPanelTab {
		public bool OpenNextUnreadMessage() {
			ISet<string> unreadMsgIds = this.GetUnreadMessages();
			if( unreadMsgIds.Count() == 0 ) {
				return false;
			}

			this.OpenMessage( unreadMsgIds.First(), false );
			return true;
		}

		public void SetAllMessagesRead() {
		}

		public void SetCurrentMessagesRead() {
		}


		////

		public void OpenMessage( string id, bool exclusively ) {
			if( exclusively ) {
				this.CloseAllMessages();
			}
			this.MessageElems[ id ].Open( true );
		}

		public void CloseAllMessages() {
			foreach( var msgElem in this.MessageElems.Values ) {
				if( msgElem.IsOpen ) {
					msgElem.Close( false );
				}
			}
		}
	}
}

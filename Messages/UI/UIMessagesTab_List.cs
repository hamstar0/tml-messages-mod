using System;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using ModControlPanel.Internals.ControlPanel;
using Messages.Definitions;


namespace Messages.UI {
	partial class UIMessagesTab : UIControlPanelTab {
		public void AddMessage( Message message ) {
			var newMsgElem = new UIMessage( message );
			newMsgElem.OnOpen += () => this.OnOpenMessage( newMsgElem );

			int idx = UIMessage.GetMessageIndexInList( this.MessagesElemsList, newMsgElem );

			//

			this.MessagesElemsList.Insert( idx, newMsgElem );

			this.MessagesDisplayListElem?.Clear();
			this.MessagesDisplayListElem?.AddRange( this.MessagesElemsList );
			this.MessagesDisplayListElem?.UpdateOrder();

			this.Recalculate();
		}

		public void RemoveMessage( string title ) {
			int idx;
			for( idx=0; idx<this.MessagesElemsList.Count; idx++ ) {
				UIMessage obj = this.MessagesElemsList[idx] as UIMessage;

				if( obj.Message.Title == title ) {
					break;
				}
			}

			UIElement item = this.MessagesElemsList[ idx ];
			this.MessagesElemsList.RemoveAt( idx );

			this.MessagesDisplayListElem?.Remove( item );
			this.MessagesDisplayListElem?.UpdateOrder();

			this.Recalculate();
		}

		public void ClearMessages() {
			for( int idx = 0; idx<this.MessagesElemsList.Count; idx++ ) {
				UIMessage obj = this.MessagesElemsList[idx] as UIMessage;

				obj?.Parent?.RemoveChild( obj );
				obj?.Remove();
			}

			this.MessagesElemsList.Clear();

			this.MessagesDisplayListElem?.Clear();
			this.MessagesDisplayListElem?.UpdateOrder();

			this.Recalculate();
		}


		////////////////

		private void OnOpenMessage( UIMessage messageElem ) {
			this.RecentMessage = messageElem;

			foreach( UIElement elem in this.MessagesElemsList ) {
				if( elem != messageElem ) {
					((UIMessage)elem).Close( false );
				}
			}

			var myplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );
			myplayer.RecordReadMessage( messageElem.Message.ID );
		}
	}
}

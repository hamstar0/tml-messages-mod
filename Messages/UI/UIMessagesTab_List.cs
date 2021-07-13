using System;
using Terraria;
using Terraria.UI;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;
using ModControlPanel.Internals.ControlPanel;
using Messages.Definitions;


namespace Messages.UI {
	partial class UIMessagesTab : UIControlPanelTab {
		public UIMessage GetMessageElementInList( Message message ) {
			return this.TopLevelMessageElems.GetOrDefault( message.ID );
		}


		public void AddMessageAsElementInListIf( Message message, Message parent=null ) {
			var newMsgElem = new UIMessage( message );
			newMsgElem.OnOpen += () => this.OnOpenListedMessageElement( newMsgElem );

			if( parent == null ) {
				int idx = UIMessage.GetMessageIndexInList( this.TopLevelMessageElemsOrdered, newMsgElem );

				this.TopLevelMessageElemsOrdered.Insert( idx, newMsgElem );
				this.TopLevelMessageElems[ message.ID ] = newMsgElem;

				this.ListElem?.Add( newMsgElem );
			} else {
				parent.AddChild( message );
			}

			// Message -> UIMessage "MVC" handled here
			message.OnChildAdd += ( idx, nextNewMessage ) => {
				newMsgElem.AddNestedMessage( new UIMessage(nextNewMessage) );
			};
		}

		public void RemoveMessageElementFromList( Message message ) {
			int idx;
			for( idx=0; idx<this.TopLevelMessageElemsOrdered.Count; idx++ ) {
				UIMessage obj = this.TopLevelMessageElemsOrdered[idx];

				if( obj.Message.ID == message.ID ) {
					this.RemoveMessageElementFromlistAt( idx );
				}
			}
		}

		private void RemoveMessageElementFromlistAt( int idx  ) {
			UIMessage msg = this.TopLevelMessageElemsOrdered[ idx ];
			UIElement item = this.TopLevelMessageElems[ msg.Message.ID ];

			this.TopLevelMessageElems.Remove( msg.Message.ID );
			this.TopLevelMessageElemsOrdered.RemoveAt( idx );

			if( this.ListElem?.Remove(item) ?? false ) {
				this.ListElem?.UpdateOrder();

				this.Recalculate();
			}
		}

		public void ClearMessageElementsList() {
			this.TopLevelMessageElemsOrdered.Clear();
			this.TopLevelMessageElems.Clear();

			this.ListElem?.Clear();

			this.Recalculate();
		}


		////////////////

		private void OnOpenListedMessageElement( UIMessage messageElem ) {
			this.RecentMessage = messageElem;
		}
	}
}

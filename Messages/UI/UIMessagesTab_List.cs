using System;
using Terraria;
using Terraria.UI;
using ModLibsCore.Libraries.Debug;
using ModUtilityPanels.Internals.UtilityPanels;
using Messages.Definitions;


namespace Messages.UI {
	partial class UIMessagesTab : UIUtilityPanelsTab {
		private UIMessage CreateOrGetMessageElem( Message message ) {
			if( this.MessageElems.ContainsKey(message.ID) ) {
				return this.MessageElems[ message.ID ];
			}

			var msgElem = new UIMessage( message, this );
			msgElem.OnBodyView += () => this.OnOpenListedMessageElement( msgElem );

			this.MessageElems[ message.ID ] = msgElem;

			return msgElem;
		}


		////////////////

		public UIMessage AddMessageAsElementInListIf( Message message, Message parent=null ) {
//LogLibraries.Log( "AddMessageAsElementInListIf " + message.ID + ", parent: "+parent?.ID );
			UIMessage msgElem = this.CreateOrGetMessageElem( message );

			if( parent == null ) {
				int idx = UIMessage.GetMessageIndexInList( this.TopLevelMessageElemsOrdered, msgElem );

				this.TopLevelMessageElemsOrdered.Insert( idx, msgElem );

				this.MessagesTreeList?.Add( msgElem );
			} else {
				//this.GetMessageElementInList( parent )
				//	.AddNestedMessageElem( newMsgElem );
			}

//LogLibraries.Log( "added child event for "+message.ID );
			// Message -> UIMessage "MVC" handled here
			message.OnChildAdd += ( idx, nextNewMessage ) => {
				var nextNewMsgElem = this.CreateOrGetMessageElem( nextNewMessage );

				msgElem.AddChildMessageElem( nextNewMsgElem );
			};

			return msgElem;
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
			UIElement item = this.MessageElems[ msg.Message.ID ];

			this.MessageElems.Remove( msg.Message.ID );
			this.TopLevelMessageElemsOrdered.RemoveAt( idx );

			if( this.MessagesTreeList?.Remove(item) ?? false ) {
				this.MessagesTreeList?.UpdateOrder();

				this.Recalculate();
			}
		}

		public void ClearMessageElementsList() {
			this.TopLevelMessageElemsOrdered.Clear();
			this.MessageElems.Clear();

			this.MessagesTreeList?.Clear();
			this.MessageBodyList?.Clear();

			this.Recalculate();
		}


		////////////////

		private void OnOpenListedMessageElement( UIMessage messageElem ) {
			this.RecentMessageID = messageElem.Message.ID;
		}
	}
}

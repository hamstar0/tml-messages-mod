using System;
using Terraria;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Elements;


namespace Messages.UI {
	partial class UIMessage : UIThemedPanel {
		public void ToggleOpen() {
			if( this.IsOpen ) {
				this.Close( true );
			} else {
				this.Open( true, true );
			}
		}

		////////////////

		internal void Open( bool expandChildren, bool viaInterface ) {
			//this.ParentMessageElem?.Open( true );

			var mycustomplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );
			mycustomplayer.SetReadMessage( this.Message.ID );

			this.TabContainer.MessageViewPanel.RemoveAllChildren();
			this.TabContainer.MessageViewPanel.Append( this.DescriptionElem );

			if( expandChildren ) {
				this.ChildMessagesContainerElem.RemoveAllChildren();

				foreach( UIMessage msgElem in this.ChildMessageElems ) {
					this.ChildMessagesContainerElem.Append( msgElem );
				}
			}

			if( viaInterface ) {
				this.OnOpen?.Invoke();
			}

			this.Height.Set( this.CalculateInnerHeight(true) + this.CalculateNestedMessagesHeight(), 0f );

			this.IsOpen = true;
		}


		internal void Close( bool viaInterface ) {
			foreach( UIMessage msgElem in this.ChildMessageElems ) {
				msgElem.Close( viaInterface );
			}

			this.ChildMessagesContainerElem.RemoveAllChildren();

			this.IsOpen = false;

			this.Recalculate();

			this.Height.Set( this.CalculateInnerHeight(false), 0f );

			this.Recalculate();
		}
	}
}

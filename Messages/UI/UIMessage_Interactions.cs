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
				this.Open( true );
			}
		}

		////////////////

		internal void Open( bool viaInterface ) {
			this.ParentMessageElem?.Open( true );

			var mycustomplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );
			mycustomplayer.SetReadMessage( this.Message.ID );

			this.DescriptionDisplayElem.RemoveAllChildren();
			this.DescriptionDisplayElem.Append( this.DescriptionElem );

			this.ChildMessagesContainerElem.RemoveAllChildren();
			foreach( UIMessage msgElem in this.ChildMessageElems ) {
				this.ChildMessagesContainerElem.Append( msgElem );
			}

			if( viaInterface ) {
				this.OnOpen?.Invoke();
			}

			this.Recalculate();
			this.DescriptionDisplayElem.Recalculate();

			this.IsOpen = true;
		}


		internal void Close( bool viaInterface ) {
			foreach( UIMessage msgElem in this.ChildMessageElems ) {
				msgElem.Close( viaInterface );
			}

			this.IsOpen = false;

			this.DescriptionDisplayElem.RemoveAllChildren();
			this.ChildMessagesContainerElem.RemoveAllChildren();

			this.Recalculate();

			this.Height.Set( this.CalculateInnerHeight(false), 0f );

			this.Recalculate();
			this.DescriptionDisplayElem.Recalculate();
		}
	}
}

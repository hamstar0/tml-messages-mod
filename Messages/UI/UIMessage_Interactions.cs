using System;
using Terraria;
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
			this.IsOpen = true;

			var myplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );
			myplayer.RecordReadMessage( this.Message.ID );

			this.DescriptionDisplayElem.RemoveAllChildren();
			this.DescriptionDisplayElem.Append( this.DescriptionElem );

			foreach( UIMessage msgElem in this.ChildMessageElems ) {
				this.ChildMessagesContainerElem.Append( msgElem );
			}

			if( viaInterface ) {
				this.OnOpen?.Invoke();
			}

			this.Recalculate();
			this.DescriptionDisplayElem.Recalculate();
		}


		internal void Close( bool viaInterface ) {
			this.IsOpen = false;

			this.ChildMessagesContainerElem.RemoveAllChildren();

			this.Recalculate();

			this.Height.Set( this.CalculateInnerHeight(false), 0f );
		}
	}
}

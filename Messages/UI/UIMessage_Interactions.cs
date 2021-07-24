using System;
using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Elements;


namespace Messages.UI {
	partial class UIMessage : UIThemedPanel {
		public void ToggleOpen( bool recalcContainer ) {
			if( this.IsOpen ) {
				this.Close( true );
			} else {
				this.Open( true, true );
			}

			if( recalcContainer ) {
				for( UIElement parent = this.Parent; parent != null; parent = parent.Parent ) {
					if( parent is UIList ) {
						parent.Recalculate();
						break;
					}
				}
			}
		}

		////////////////

		internal void Open( bool expandChildren, bool viaInterface ) {
			var mycustomplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );
			mycustomplayer.SetReadMessage( this.Message.ID );

			this.TabContainer.MessageViewPanel.RemoveAllChildren();
			this.TabContainer.MessageViewPanel.Append( this.DescriptionElem );

			if( expandChildren ) {
				this.InitializeChildMessages();
			}

			if( viaInterface ) {
				this.OnOpen?.Invoke();
			}

			this.IsOpen = true;

			this.Recalculate();
		}


		internal void Close( bool viaInterface ) {
			foreach( UIMessage msgElem in this.ChildMessageElems ) {
				msgElem.Close( viaInterface );
			}

			this.ChildMessagesContainerElem.RemoveAllChildren();

			this.IsOpen = false;

			this.Recalculate();
		}
	}
}

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

			this.DescriptionContainerElem.Append( this.DescriptionElem );
			//this.Elements.Append( this.DescriptionElem );
			//this.Elements.Insert( 1, this.DescriptionElem );
			
//LogLibraries.Log( "Open:" + this.Message.ID+" ("+this.ChildMessageElems.Count+" children, "+this.GetHashCode()+")" );
			foreach( UIMessage msgElem in this.ChildMessageElems ) {
//LogLibraries.Log( "  Child: " + msgElem.Message.ID + " ("+this.GetHashCode()+")" );
				this.ChildMessagesContainerElem.Append( msgElem );
			}

			if( viaInterface ) {
				this.OnOpen?.Invoke();
			}

			this.Recalculate();
		}


		internal void Close( bool viaInterface ) {
			this.IsOpen = false;

			//this.RemoveChild( this.DescriptionElem );
			this.DescriptionContainerElem.RemoveChild( this.DescriptionElem );

			this.ChildMessagesContainerElem.RemoveAllChildren();

			this.Recalculate();

			this.Height.Set( this.CalculateInnerHeight(false), 0f );
		}
	}
}

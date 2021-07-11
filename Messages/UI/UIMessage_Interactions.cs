using System;
using Terraria;
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
			this.Message.IsRead = true;

			this.DescriptionContainerElem.Append( this.DescriptionElem );
			//this.Elements.Append( this.DescriptionElem );
			//this.Elements.Insert( 1, this.DescriptionElem );

			foreach( UIMessage msgElem in this.NestedMessages ) {
				this.NestedMessagesElem.Append( msgElem );
			}

			if( viaInterface ) {
				this.OnOpen?.Invoke();
			}

			this.DescriptionElem.Recalculate();
			this.Recalculate();

			float height = this.CalculateInnerHeight( true );
			float nestedHeight = this.CalculateNestedMessagesHeight();

			this.Height.Set( height, 0f );

			this.NestedMessagesElem.Top.Set( height, 0f );
			this.NestedMessagesElem.Height.Set( nestedHeight, 0f );
		}

		internal void Close( bool viaInterface ) {
			this.IsOpen = false;

			//this.RemoveChild( this.DescriptionElem );
			this.DescriptionContainerElem.RemoveChild( this.DescriptionElem );

			this.NestedMessagesElem.RemoveAllChildren();

			this.Recalculate();

			this.Height.Set( this.CalculateInnerHeight(false), 0f );
		}
	}
}

using System;
using Terraria;
using Terraria.UI;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Elements;


namespace Messages.UI {
	partial class UIMessage : UIThemedPanel {
		public float CalculateBodyHeight() {
			return UIMessage.DefaultHeight;
		}


		////////////////

		public override void Recalculate() {
			float baseHeight = this.CalculateBodyHeight();
			float childContainerHeight = 0f;

			if( this.IsOpen ) {
				this.Height.Set( Single.MaxValue, 0f );
				this.ChildMessagesContainerElem.Height.Set( Single.MaxValue, 0f );

				base.Recalculate();

				this.RecalculateChildMessagePositions( out childContainerHeight );
			}

			this.Height.Set( childContainerHeight + baseHeight, 0f );
			this.ChildMessagesContainerElem.Height.Set( childContainerHeight, 0f );

			base.Recalculate();
		}


		private void RecalculateChildMessagePositions( out float childContainerHeight ) {
			float yOffset = 0f;

/*string outMe( string input, UIElement curr ) {
	string output = curr.GetOuterDimensions().Height+", "+input;
	if( curr.Parent != null ) {
		output = outMe( output, curr.Parent );
	}
	return output;
}*/
			foreach( UIMessage msgElem in this.ChildMessageElems ) {
				msgElem.Top.Set( yOffset, 0f );
				msgElem.Recalculate();
/*LogLibraries.LogOnce( "RecalculateChilds "+this.IsOpen
	+" - y:"+yOffset+" y2:"+msgElem.Top.Pixels+" y3:"+msgElem.GetOuterDimensions().Y
	+" h1:"+msgElem.GetOuterDimensions().Height+" h2:"+msgElem.Height.Pixels
	+" - "+this.Message.ID+" = "+msgElem.Message.ID );
LogLibraries.LogOnce( "  "+outMe("", msgElem) );*/
				yOffset += msgElem.GetOuterDimensions().Height;
			}

			childContainerHeight = yOffset;
		}
	}
}

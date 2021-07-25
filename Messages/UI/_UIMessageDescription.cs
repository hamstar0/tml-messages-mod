using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using Terraria;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;
using ModLibsGeneral.Libraries.Misc;
using ModLibsGeneral.Libraries.UI;
using ModLibsUI.Classes.UI.Elements;
using ModLibsUI.Classes.UI.Theme;


namespace Messages.UI {
	class UIMessageDescription : UIThemedText {
		public const float DefaultScale = 0.8f;



		////////////////
		
		public string OriginalText { get; private set; }



		////////////////
		
		public UIMessageDescription( UITheme theme, string text )
				: base( theme, false, text, UIMessageDescription.DefaultScale, false ) {
			this.Width.Set( 0f, 1f );

			this.OriginalText = text;
		}


		////////////////

		/*public override void Recalculate() {
			if( this.Parent != null ) {
				DynamicSpriteFont font = Main.fontMouseText;    //this.Large ? Main.fontDeathText : Main.fontMouseText;
				float textWidth = this.Parent.GetInnerDimensions().Width;	//this.Parent.Width.Pixels?
				float textWidthScaled = textWidth * (1f / UIMessageDescription.DefaultScale);

				IList<string> lines = FontLibraries.FitText( font, this.OriginalText, (int)textWidthScaled );
				string text = lines.ToStringJoined( "\n" );

				this.SetText( text );
//Vector2 textDim = font.MeasureString(  text ) * UIMessageDescription.DefaultScale;
//LogLibraries.Log( "dim: " + textDim+", text: "+text+", lines: "+lines.FirstOrDefault() );
			}

			base.Recalculate();
//LogLibraries.Log( "hier: "+UILibraries.GetHierarchyData(this).Select(e=>e.Dimension).ToStringJoined("\n  ") );
		}*/
	}
}

using System;
using Terraria;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Elements;
using ModLibsUI.Classes.UI.Theme;


namespace Messages.UI {
	class UIMessageTitle : UIThemedText {
		public const float DefaultScale = 1.1f;

		public UIMessageTitle( UITheme theme, string text )
				: base( theme, false, text, UIMessageTitle.DefaultScale, false ) {
			this.Width.Set( 0f, 1f );
		}
	}
}

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Theme;
using ModControlPanel.Internals.ControlPanel;


namespace Messages.UI {
	partial class UIMessagesTab : UIControlPanelTab {
		private IDictionary<string, UIMessage> MessageElems = new ConcurrentDictionary<string, UIMessage>();

		private IList<UIMessage> TopLevelMessageElemsOrdered = new List<UIMessage>();

		internal UIMessage RecentMessage = null;

		////

		private UIList ListElem;
		private UIHideableScrollbar Scrollbar;



		////////////////

		public UIMessagesTab( UITheme theme ) {
			this.Theme = theme;

			this.Width.Set( 0f, 1f );
			this.Height.Set( 0f, 1f );
		}


		////////////////

		public override void Draw( SpriteBatch spriteBatch ) {
			bool listChanged;

			try {
				this.Scrollbar.IsHidden = UIHideableScrollbar.IsScrollbarHidden(
					(int)this.ListElem.Height.Pixels,
					this.ListElem.Parent
				);

				if( this.Scrollbar.IsHidden ) {
					listChanged = this.ListElem.Width.Pixels != 0f;
					this.ListElem.Width.Pixels = 0f;
				} else {
					listChanged = this.ListElem.Width.Pixels != -25f;
					this.ListElem.Width.Pixels = -25f;
				}

				if( listChanged ) {
					this.Recalculate();
					this.ListElem.Recalculate();
				}
			} catch { }

			base.Draw( spriteBatch );
		}
	}
}

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Theme;
using ModLibsUI.Classes.UI.Elements;
using ModControlPanel.Internals.ControlPanel;


namespace Messages.UI {
	partial class UIMessagesTab : UIControlPanelTab {
		public const int MyWidth = 800;



		////////////////
		
		private IDictionary<string, UIMessage> MessageElems = new ConcurrentDictionary<string, UIMessage>();

		private IList<UIMessage> TopLevelMessageElemsOrdered = new List<UIMessage>();

		internal string RecentMessageID = null;

		////

		private UIList ListElem;
		private UIThemedPanel Toolbar;
		private UITextPanelButton GetUnreadButton;
		private UITextPanelButton AllReadButton;
		private UITextPanelButton ThisUnreadButton;
		private UIThemedPanel MessageTreePanel;

		internal UIThemedPanel MessageViewPanel;

		private UIHideableScrollbar Scrollbar;



		////////////////

		public UIMessagesTab( UITheme theme ) : base( theme, UIMessagesTab.MyWidth ) {
			this.Theme = theme;

			this.PreInitializeMe();
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

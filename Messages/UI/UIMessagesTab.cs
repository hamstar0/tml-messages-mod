using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Theme;
using ModLibsUI.Classes.UI.Elements;
using ModUtilityPanels.Internals.UtilityPanels;


namespace Messages.UI {
	partial class UIMessagesTab : UIUtilityPanelsTab {
		public const int MyWidth = 800;



		////////////////
		
		private IDictionary<string, UIMessage> MessageElems = new ConcurrentDictionary<string, UIMessage>();

		private IList<UIMessage> TopLevelMessageElemsOrdered = new List<UIMessage>();

		internal string RecentMessageID = null;

		////

		private UIThemedPanel Toolbar;
		private UITextPanelButton GetUnreadButton;
		private UITextPanelButton AllReadButton;
		private UITextPanelButton ThisUnreadButton;

		private UIThemedPanel MessageTreePanel;
		private UIThemedPanel MessageBodyPanel;

		private UIList MessagesTreeList;
		internal UIList MessageBodyList;

		private UIHideableScrollbar MessagesTreeListScrollbar;
		private UIHideableScrollbar MessageBodyListScrollbar;



		////////////////

		public UIMessagesTab( UITheme theme ) : base( theme, UIMessagesTab.MyWidth ) {
			this.Theme = theme;

			this.OnOpenTab.Add( prevTab => {
				if( prevTab != this ) {
					this.OpenNextUnreadMessage();
				}

				MessagesMod.Instance.HideAlert();
			} );

			this.PreInitializeMe();
		}

		
		////////////////

		public override void Draw( SpriteBatch spriteBatch ) {
			try {
				this.UpdateTreeScrollbar();
				this.UpdateBodyScrollbar();
			} catch { }

			base.Draw( spriteBatch );
		}
	}
}

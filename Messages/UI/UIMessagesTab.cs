using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Theme;
using ModControlPanel.Internals.ControlPanel;


namespace Messages.UI {
	partial class UIMessagesTab : UIControlPanelTab {
		private IDictionary<string, UIMessage> TopLevelMessageElems = new ConcurrentDictionary<string, UIMessage>();

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

		public override void OnInitializeMe() {
			var label = new UIText( "Messages:" );
			label.Top.Set( 0f, 0f );
			label.Left.Set( 0f, 0f );
			this.Append( (UIElement)label );

			var messagesPanel = new UIPanel();
			messagesPanel.Top.Set( 24f, 0f );
			messagesPanel.Width.Set( 0f, 1f );
			messagesPanel.Height.Set( 472f, 0f );
			messagesPanel.HAlign = 0f;
			messagesPanel.SetPadding( 4f );
			//modListPanel.PaddingTop = 0.0f;
			messagesPanel.BackgroundColor = this.Theme.ListBgColor;
			messagesPanel.BorderColor = this.Theme.ListEdgeColor;
			this.Append( (UIElement)messagesPanel );

			this.ListElem = new UIList();
			this.ListElem.Left.Set( 0f, 0f );
			this.ListElem.Width.Set( -25f, 1f );
			this.ListElem.Height.Set( 0f, 1f );
			this.ListElem.HAlign = 0f;
			this.ListElem.ListPadding = 4f;
			this.ListElem.SetPadding( 0f );
			messagesPanel.Append( (UIElement)this.ListElem );

			this.Scrollbar = new UIHideableScrollbar( true );
			this.Scrollbar.Top.Set( 8f, 0f );
			this.Scrollbar.Left.Set( -24f, 1f );
			this.Scrollbar.Height.Set( -16f, 1f );
			this.Scrollbar.SetView( 100f, 1000f );
			this.Scrollbar.HAlign = 0f;
			messagesPanel.Append( (UIElement)this.Scrollbar );

			this.ListElem.SetScrollbar( this.Scrollbar );

			//

			this.ListElem.AddRange( this.TopLevelMessageElemsOrdered );
			this.ListElem.UpdateOrder();
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

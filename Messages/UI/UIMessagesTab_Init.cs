﻿using System;
using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Elements;
using ModControlPanel.Internals.ControlPanel;


namespace Messages.UI {
	partial class UIMessagesTab : UIControlPanelTab {
		private void PreInitializeMe() {
			this.Toolbar = this.InitializeToolbar();
			this.MessageTreePanel = this.InitializeMessageTreePanel();
			this.MessageViewPanel = this.InitializeMessageViewPanel();

			this.InitializeMessagesTree(
				this.MessageTreePanel,
				out this.ListElem,
				out this.Scrollbar
			);
		}


		////////////////

		public override void OnInitializeMe() {
			this.Width.Set( 0f, 1f );
			this.Height.Set( 0f, 1f );

			this.Append( (UIElement)this.Toolbar );
			this.Append( (UIElement)this.MessageTreePanel );
			this.Append( (UIElement)this.MessageViewPanel );
		}


		////////////////

		private UIThemedPanel InitializeToolbar() {
			var toolbar = new UIThemedPanel( this.Theme, false );
			toolbar.Top.Set( -4f, 0f );
			toolbar.Width.Set( 0f, 1f );
			toolbar.Height.Set( 28f, 0f );

			var test = new UITextPanelButton( this.Theme, "test" );
			test.Top.Set( -16f, 1f );
			test.Left.Set( -12f, 0f );
			toolbar.Append( (UIElement)test );

			return toolbar;
		}

		private UIThemedPanel InitializeMessageTreePanel() {
			var messageTreePanel = new UIThemedPanel( this.Theme, false );
			messageTreePanel.Top.Set( 24f, 0f );
			messageTreePanel.Width.Set( 0f, 0.35f );
			messageTreePanel.Height.Set( 472f, 0f );
			messageTreePanel.HAlign = 0f;
			messageTreePanel.SetPadding( 4f );
			//messageTreePanel.PaddingTop = 0.0f;
			messageTreePanel.BackgroundColor = this.Theme.ListBgColor;
			messageTreePanel.BorderColor = this.Theme.ListEdgeColor;

			return messageTreePanel;
		}

		private UIThemedPanel InitializeMessageViewPanel() {
			var messageViewPanel = new UIThemedPanel( this.Theme, false );
			messageViewPanel.Top.Set( 24f, 0f );
			messageViewPanel.Left.Set( 0f, 0.35f );
			messageViewPanel.Width.Set( 0f, 0.65f );
			messageViewPanel.Height.Set( 472f, 0f );
			messageViewPanel.HAlign = 0f;
			messageViewPanel.SetPadding( 4f );

			return messageViewPanel;
		}

		private void InitializeMessagesTree(
					UIPanel messagesPanel,
					out UIList listElem,
					out UIHideableScrollbar scrollbar) {
			listElem = new UIList();
			listElem.Left.Set( 0f, 0f );
			listElem.Width.Set( -25f, 1f );
			listElem.Height.Set( 0f, 1f );
			listElem.HAlign = 0f;
			listElem.ListPadding = 4f;
			listElem.SetPadding( 0f );
			messagesPanel.Append( (UIElement)listElem );

			scrollbar = new UIHideableScrollbar( true );
			scrollbar.Top.Set( 8f, 0f );
			scrollbar.Left.Set( -24f, 1f );
			scrollbar.Height.Set( -16f, 1f );
			scrollbar.SetView( 100f, 1000f );
			scrollbar.HAlign = 0f;
			messagesPanel.Append( (UIElement)this.Scrollbar );

			listElem.SetScrollbar( this.Scrollbar );

			//

			listElem.AddRange( this.TopLevelMessageElemsOrdered );
			listElem.UpdateOrder();
		}
	}
}

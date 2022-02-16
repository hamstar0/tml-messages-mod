using System;
using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Elements;
using ModUtilityPanels.Classes.UI;


namespace Messages.UI {
	partial class UIMessagesTab : UIUtilityPanelsTab {
		private void PreInitializeMe() {
			this.Toolbar = this.InitializeToolbar(
				out this.GetUnreadButton,
				out this.AllReadButton,
				out this.ThisUnreadButton
			);
			this.InitializeMessageTreeList(
				out this.MessagesTreeList,
				out this.MessagesTreeListScrollbar
			);
			this.InitializeMessageBodyList(
				out this.MessageBodyList,
				out this.MessageBodyListScrollbar
			);
			this.MessageTreePanel = this.InitializeMessageTreePanel();
			this.MessageBodyPanel = this.InitializeMessageBodyPanel();
		}


		////////////////

		public override void OnInitializeMe() {
			this.Width.Set( 0f, 1f );
			this.Height.Set( 0f, 1f );

			this.MessageTreePanel.Append( (UIElement)this.MessagesTreeList );
			this.MessageTreePanel.Append( (UIElement)this.MessagesTreeListScrollbar );

			this.MessageBodyPanel.Append( (UIElement)this.MessageBodyList );
			this.MessageBodyPanel.Append( (UIElement)this.MessageBodyListScrollbar );

			this.Append( (UIElement)this.Toolbar );
			this.Append( (UIElement)this.MessageTreePanel );
			this.Append( (UIElement)this.MessageBodyPanel );

			//

			this.Recalculate();
		}


		////////////////

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

		private UIThemedPanel InitializeMessageBodyPanel() {
			var messageTreePanel = new UIThemedPanel( this.Theme, false );
			messageTreePanel.Top.Set( 24f, 0f );
			messageTreePanel.Left.Set( 0f, 0.35f );
			messageTreePanel.Width.Set( 0f, 0.65f );
			messageTreePanel.Height.Set( 472f, 0f );
			messageTreePanel.HAlign = 0f;
			messageTreePanel.SetPadding( 4f );
			//messageTreePanel.PaddingTop = 0.0f;
			messageTreePanel.BackgroundColor = this.Theme.ListBgColor;
			messageTreePanel.BorderColor = this.Theme.ListEdgeColor;

			return messageTreePanel;
		}

		
		private void InitializeMessageTreeList( out UIList listElem, out UIHideableScrollbar scrollbar) {
			listElem = new UIList();
			listElem.Left.Set( 0f, 0f );
			listElem.Width.Set( -25f, 1f );
			listElem.Height.Set( 0f, 1f );
			listElem.HAlign = 0f;
			listElem.ListPadding = 4f;
			listElem.SetPadding( 0f );

			scrollbar = new UIHideableScrollbar( true );
			scrollbar.Top.Set( 8f, 0f );
			scrollbar.Left.Set( -24f, 1f );
			scrollbar.Height.Set( -16f, 1f );
			scrollbar.SetView( 100f, 1000f );
			scrollbar.HAlign = 0f;

			listElem.SetScrollbar( scrollbar );

			//

			listElem.AddRange( this.TopLevelMessageElemsOrdered );
			listElem.UpdateOrder();
		}

		private void InitializeMessageBodyList( out UIList listElem, out UIHideableScrollbar scrollbar ) {
			listElem = new UIList();
			listElem.Left.Set( 0f, 0f );
			listElem.Width.Set( -25f, 1f );
			listElem.Height.Set( 0f, 1f );
			listElem.HAlign = 0f;
			listElem.ListPadding = 4f;
			listElem.SetPadding( 0f );

			scrollbar = new UIHideableScrollbar( true );
			scrollbar.Top.Set( 8f, 0f );
			scrollbar.Left.Set( -24f, 1f );
			scrollbar.Height.Set( -16f, 1f );
			scrollbar.SetView( 100f, 1000f );
			scrollbar.HAlign = 0f;

			listElem.SetScrollbar( scrollbar );
		}
	}
}

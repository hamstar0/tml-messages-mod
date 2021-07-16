using System;
using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using ModLibsCore.Libraries.Debug;
using ModControlPanel.Internals.ControlPanel;


namespace Messages.UI {
	partial class UIMessagesTab : UIControlPanelTab {
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
	}
}

using System;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Elements;
using ModUtilityPanels.Internals.UtilityPanels;
using Messages.Logic;


namespace Messages.UI {
	partial class UIMessagesTab : UIUtilityPanelsTab {
		private UIThemedPanel InitializeToolbar(
					out UITextPanelButton getUnreadButton,
					out UITextPanelButton allReadButton,
					out UITextPanelButton thisUnreadButton ) {
					//out UITextPanelButton modsCatButton,
					//out UITextPanelButton tipsCatButton,
					//out UITextPanelButton gameCatButton,
					//out UITextPanelButton loreCatButton ) {
			var mngr = ModContent.GetInstance<MessageManager>();
			float leftOrigin = -12f;
			
			var toolbar = new UIThemedPanel( this.Theme, false );
			toolbar.Top.Set( -4f, 0f );
			toolbar.Width.Set( 0f, 1f );
			toolbar.Height.Set( 28f, 0f );

			//

			getUnreadButton = new UITextPanelButton( this.Theme, "Next Unread", 0.85f );
			getUnreadButton.Top.Set( -10f, 0f );
			getUnreadButton.Left.Set( leftOrigin + 4f, 0f );
			getUnreadButton.Width.Set( 108f, 0f );
			getUnreadButton.OnClick += (_, __) => this.OpenNextUnreadMessage();
			toolbar.Append( (UIElement)getUnreadButton );
			
			allReadButton = new UITextPanelButton( this.Theme, "Set All Read", 0.85f );
			allReadButton.Top.Set( -10f, 0f );
			allReadButton.Left.Set( leftOrigin + 116f, 0f );
			allReadButton.Width.Set( 108f, 0f );
			allReadButton.OnClick += ( _, __ ) => this.SetAllMessagesRead();
			toolbar.Append( (UIElement)allReadButton );

			thisUnreadButton = new UITextPanelButton( this.Theme, "Set Current Unread", 0.85f );
			thisUnreadButton.Top.Set( -10f, 0f );
			thisUnreadButton.Left.Set( leftOrigin + 228f, 0f );
			thisUnreadButton.Width.Set( 108f, 0f );
			thisUnreadButton.OnClick += ( _, __ ) => this.SetCurrentMessageUnread();
			toolbar.Append( (UIElement)thisUnreadButton );
			
			//

			var catLabel = new UIThemedText( this.Theme, false, "Categories:", 0.85f );
			catLabel.Top.Set( -8f, 1f );
			catLabel.Left.Set( -340f, 1f );
			catLabel.Width.Set( 108f, 0f );
			toolbar.Append( (UIElement)catLabel );

			//

			var modsCatButton = new UITextPanelButton( this.Theme, "Mods", 0.85f );
			modsCatButton.Top.Set( -10f, 0f );
			modsCatButton.Left.Set( -372f + 128f, 1f );
			modsCatButton.Width.Set( 60f, 0f );
			modsCatButton.OnClick += ( _, __ ) => this.OpenMessage( mngr.ModInfoCategoryMsg.ID, true );
			toolbar.Append( (UIElement)modsCatButton );

			var tipsCatButton = new UITextPanelButton( this.Theme, "Hints", 0.85f );
			tipsCatButton.Top.Set( -10f, 0f );
			tipsCatButton.Left.Set( -372f + 192f, 1f );
			tipsCatButton.Width.Set( 60f, 0f );
			tipsCatButton.OnClick += ( _, __ ) => this.OpenMessage( mngr.HintsTipsCategoryMsg.ID, true );
			toolbar.Append( (UIElement)tipsCatButton );

			var gameCatButton = new UITextPanelButton( this.Theme, "Game", 0.85f );
			gameCatButton.Top.Set( -10f, 0f );
			gameCatButton.Left.Set( -372f + 256f, 1f );
			gameCatButton.Width.Set( 60f, 0f );
			gameCatButton.OnClick += ( _, __ ) => this.OpenMessage( mngr.GameInfoCategoryMsg.ID, true );
			toolbar.Append( (UIElement)gameCatButton );
			toolbar.Append( (UIElement)tipsCatButton );

			var loreCatButton = new UITextPanelButton( this.Theme, "Lore", 0.85f );
			loreCatButton.Top.Set( -10f, 0f );
			loreCatButton.Left.Set( -372f + 320f, 1f );
			loreCatButton.Width.Set( 60f, 0f );
			loreCatButton.OnClick += ( _, __ ) => this.OpenMessage( mngr.StoryLoreCategoryMsg.ID, true );
			toolbar.Append( (UIElement)loreCatButton );

			return toolbar;
		}
	}
}

using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Elements;
using ModLibsUI.Classes.UI.Theme;


namespace Messages.UI {
	class UITitleText : UIThemedText {
		public const float DefaultScale = 1.1f;

		public UITitleText( UITheme theme, string text )
				: base( theme, false, text, UITitleText.DefaultScale, false ) {
			this.Width.Set( 0f, 1f );
		}
	}

	
	class UIDescriptionText : UIThemedText {
		public const float DefaultScale = 0.8f;

		public UIDescriptionText( UITheme theme, string text )
				: base( theme, false, text, UIDescriptionText.DefaultScale, false ) {
			this.Width.Set( 0f, 1f );
		}
	}




	partial class UIMessage : UIThemedPanel {
		private void OnInitializeMe() {
			this.Width.Set( 0f, 1f );
			this.Height.Set( UIMessage.DefaultHeight, 0f );
			this.HAlign = 0f;
			this.SetPadding( 0f );

			//
			
			this.TreeIcon = new UIText( "+" );
			this.TreeIcon.HAlign = 0f;
			this.TreeIcon.Left.Set( -8f, 0f );
			this.TreeIcon.Width.Set( 0f, 1f );
			this.TreeIcon.TextColor = Color.White;
			this.Append( this.TreeIcon );
			
			this.InfoContainer = new UIElement();
			{
				this.TitleElem = new UITitleText( this.Theme, this.Message.Title );
				this.TitleElem.TextColor = Color.Yellow;
				this.InfoContainer.Append( this.TitleElem );
			}
			this.InfoContainer.OnClick += (_, __) => this.ToggleOpen();
			this.InfoContainer.Top.Set( 0f, 0f );
			this.InfoContainer.Width.Set( 0f, 1f );
			this.InfoContainer.PaddingTop = 8f;
			this.InfoContainer.Height.Set( UIMessage.DefaultHeight, 0f );
			this.Append( this.InfoContainer );

			this.DescriptionElem = new UIDescriptionText( this.Theme, this.Message.Description );
			this.DescriptionElem.TextColor = Color.White;

			this.ChildMessagesContainerElem = new UIElement();
			this.ChildMessagesContainerElem.Top.Set( UIMessage.DefaultHeight, 0f );
			this.ChildMessagesContainerElem.Width.Set( 0f, 1f );
			this.ChildMessagesContainerElem.SetPadding( 8f );
			this.Append( this.ChildMessagesContainerElem );

			this.Recalculate();

			this.TreeIcon.PaddingRight = this.GetOuterDimensions().Width - 24f;	//ugly!
		}


		////////////////

		public UIDescriptionText GetDescriptionElement() {
			var descElem = new UIDescriptionText( this.Theme, this.Message.Description );
			descElem.TextColor = Color.White;

			return descElem;
		}
	}
}

using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;
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
			this.SetPadding( 0f );

			//
			
			this.InfoContainer = new UIElement();
			{
				this.TitleElem = new UITitleText( this.Theme, this.Message.Title );
				this.TitleElem.TextColor = Color.Yellow;
				this.InfoContainer.Append( this.TitleElem );

				this.DescriptionElem = new UIDescriptionText( this.Theme, this.Message.Description );
				this.DescriptionElem.TextColor = Color.White;
			}
			this.InfoContainer.OnClick += (_, __) => this.ToggleOpen();
			this.InfoContainer.Width.Set( 0f, 1f );
			this.InfoContainer.Height.Set( UIMessage.DefaultHeight + this.DescriptionElem.GetDimensions().Y, 0f );
			this.InfoContainer.SetPadding( 8f );
			this.Append( this.InfoContainer );

			this.ChildMessagesContainerElem = new UIElement();
			this.ChildMessagesContainerElem.Top.Set( UIMessage.DefaultHeight, 0f );
			this.ChildMessagesContainerElem.Width.Set( 0f, 1f );
			this.ChildMessagesContainerElem.SetPadding( 8f );
			this.Append( this.ChildMessagesContainerElem );

			this.Recalculate();
		}


		////////////////

		public UIDescriptionText GetDescriptionElement() {
			var descElem = new UIDescriptionText( this.Theme, this.Message.Description );
			descElem.TextColor = Color.White;

			return descElem;
		}
	}
}

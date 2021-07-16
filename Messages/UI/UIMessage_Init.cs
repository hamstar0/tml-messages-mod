using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Elements;


namespace Messages.UI {
	partial class UIMessage : UIThemedPanel {
		private void OnInitializeMe() {
			this.Width.Set( 0f, 1f );
			this.Height.Set( UIMessage.DefaultHeight, 0f );
			this.SetPadding( 0f );

			//

			var infoContainer = new UIElement();
			{
				this.TitleElem = new UIThemedText( this.Theme, false, this.Message.Title, UIMessage.DefaultTitleScale );
				this.TitleElem.TextColor = Color.Yellow;
				this.TitleElem.Width.Set( 0f, 1f );
				infoContainer.Append( this.TitleElem );

				this.DescriptionContainerElem = new UIElement();
				this.DescriptionContainerElem.Top.Set( UIMessage.DefaultHeight, 0f );
				infoContainer.Append( this.DescriptionContainerElem );

				this.DescriptionElem = new UIThemedText( this.Theme, false, this.Message.Description, UIMessage.DefaultDescScale );
				this.DescriptionElem.TextColor = Color.White;
				this.DescriptionElem.Width.Set( 0f, 1f );
				//infoContainer.Append( this.DescriptionElem );
			}
			infoContainer.OnClick += (_, __) => this.ToggleOpen();
			infoContainer.Width.Set( 0f, 1f );
			infoContainer.Height.Set( UIMessage.DefaultHeight + this.DescriptionElem.GetDimensions().Y, 0f );
			infoContainer.SetPadding( 8f );
			this.Append( infoContainer );

			this.ChildMessagesContainerElem = new UIElement();
			this.ChildMessagesContainerElem.Top.Set( UIMessage.DefaultHeight, 0f );
			this.ChildMessagesContainerElem.Width.Set( 0f, 1f );
			this.ChildMessagesContainerElem.SetPadding( 8f );
			this.Append( this.ChildMessagesContainerElem );

			//

			this.DescriptionHeight = Main.fontMouseText.MeasureString( this.Message.Description ).Y;
		}
	}
}

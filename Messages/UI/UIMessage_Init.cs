using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;
using ModLibsGeneral.Libraries.Misc;
using ModLibsUI.Classes.UI.Elements;


namespace Messages.UI {
	partial class UIMessage : UIThemedPanel {
		private void OnInitializeMe() {
			this.Width.Set( 0f, 1f );
			this.MinHeight.Set( UIMessage.DefaultHeight, 0f );
			this.Height.Set( UIMessage.DefaultHeight, 0f );
			this.HAlign = 0f;
			this.SetPadding( 0f );

			if( this.Message.Color.HasValue ) {
				this.BackgroundColor = this.Message.Color.Value;
				this.BorderColor = Color.Lerp( this.Message.Color.Value, Color.Black, 0.65f );
			}

			//

			this.InfoContainer = new UIElement();
			{
				this.TreeIconElem = new UIThemedText( this.Theme, false, "+", true );
				this.TreeIconElem.TextColor = Color.White;
				this.InfoContainer.Append( this.TreeIconElem );
				
				this.UnreadTextElem = new UIThemedText( this.Theme, false, "" + this.UnreadHere.Count, true, 0.75f );
				this.UnreadTextElem.MarginTop = -6f;
				this.UnreadTextElem.MarginLeft = 4f;
				this.UnreadTextElem.TextColor = this.UnreadHere.Count == 0 ? Color.Gray : Color.Yellow;
				this.InfoContainer.Append( this.UnreadTextElem );

				this.TitleElem = this.CreateTitleElement();
				this.InfoContainer.Append( this.TitleElem );
			}
			this.InfoContainer.OnClick += (_, __) => this.SelectMessageFromList( true );
			this.InfoContainer.Top.Set( 0f, 0f );
			this.InfoContainer.Width.Set( 0f, 1f );
			this.InfoContainer.PaddingTop = 8f;
			this.InfoContainer.Height.Set( UIMessage.DefaultHeight, 0f );
			this.Append( this.InfoContainer );

			//

			this.DescriptionElem = this.CreateDescriptionElement( out this.DescriptionHeight );

			//

			this.ChildMessagesContainerElem = this.CreateChildContainerElement();
			this.Append( this.ChildMessagesContainerElem );

			//

			this.Recalculate();

			this.TreeIconElem.PaddingRight = this.GetOuterDimensions().Width / 2f;	//ugly!
			this.UnreadTextElem.PaddingRight = this.GetOuterDimensions().Width / 2f;    //ugly!
		}


		////////////////

		private void InitializeChildMessages() {
			this.ChildMessagesContainerElem.RemoveAllChildren();

			foreach( UIMessage msgElem in this.ChildMessageElems ) {
				this.ChildMessagesContainerElem.Append( msgElem );
			}
		}


		////////////////

		private UIThemedText CreateTitleElement() {
			float scale = 0.8f;
			int maxWidth = 200;
			maxWidth = (int)((float)maxWidth * (1f / scale));

			IList<string> titleLines = FontLibraries.FitText( Main.fontMouseText, this.Message.Title, maxWidth );
			string title = titleLines.FirstOrDefault() ?? "";
			if( titleLines.Count >= 2 ) {
				title += "...";
			}

			var elem = new UIThemedText( this.Theme, false, title, true, scale, false );
			elem.TextColor = Color.Yellow;
			elem.Width.Set( 0f, 1f );
			elem.HAlign = 0f;

			return elem;
		}

		private UIThemedText CreateDescriptionElement( out float height ) {
			float descScale = 0.8f;

			IList<string> descLines = FontLibraries.FitText( Main.fontMouseText, this.Message.Description, 560 );
			string desc = descLines.ToStringJoined( "\n" );

			height = Main.fontMouseText.MeasureString( desc ).Y;
			height *= 1f / descScale;

			var elem = new UIThemedText( this.Theme, false, desc, true, descScale, false );
			elem.TextColor = Color.White;
			elem.Width.Set( 0f, 1f );
			elem.Height.Set( this.DescriptionHeight, 0f );
			elem.HAlign = 0f;

			return elem;
		}

		private UIElement CreateChildContainerElement() {
			var elem = new UIElement();
			elem.Top.Set( UIMessage.DefaultHeight - 8f, 0f );
			elem.Width.Set( 0f, 1f );
			elem.SetPadding( 8f );
			elem.PaddingRight = 0f;

			return elem;
		}
	}
}

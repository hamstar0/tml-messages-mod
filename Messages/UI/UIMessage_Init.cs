using System;
using System.Collections.Generic;
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

			//
			
			this.InfoContainer = new UIElement();
			{
				this.TreeIconElem = new UIThemedText( this.Theme, false, "+" );
				this.TreeIconElem.TextColor = Color.White;
				this.InfoContainer.Append( this.TreeIconElem );
				
				this.UnreadTextElem = new UIThemedText( this.Theme, false, "" + this.UnreadHere.Count, 0.75f );
				this.UnreadTextElem.MarginTop = -6f;
				this.UnreadTextElem.MarginLeft = 4f;
				this.UnreadTextElem.TextColor = this.UnreadHere.Count == 0 ? Color.Gray : Color.Yellow;
				this.InfoContainer.Append( this.UnreadTextElem );

				this.TitleElem = new UIThemedText( this.Theme, false, this.Message.Title, 1.1f, false );
				this.TitleElem.TextColor = Color.Yellow;
				this.TitleElem.Width.Set( 0f, 1f );
				this.TitleElem.HAlign = 0f;
				this.InfoContainer.Append( this.TitleElem );
			}
			this.InfoContainer.OnClick += (_, __) => this.SelectMessageFromList( true );
			this.InfoContainer.Top.Set( 0f, 0f );
			this.InfoContainer.Width.Set( 0f, 1f );
			this.InfoContainer.PaddingTop = 8f;
			this.InfoContainer.Height.Set( UIMessage.DefaultHeight, 0f );
			this.Append( this.InfoContainer );

			//

			float descScale = 0.8f;
			IList<string> descLines = FontLibraries.FitText( Main.fontMouseText, this.Message.Description, 560 );
			string desc = descLines.ToStringJoined( "\n" );

			this.DescriptionHeight = Main.fontMouseText.MeasureString( desc ).Y;
			this.DescriptionHeight *= 1f / descScale;

			this.DescriptionElem = new UIThemedText( this.Theme, false, desc, descScale, false );
			this.DescriptionElem.TextColor = Color.White;
			this.DescriptionElem.Width.Set( 0f, 1f );
			this.DescriptionElem.Height.Set( this.DescriptionHeight, 0f );
			this.DescriptionElem.HAlign = 0f;

			//

			this.ChildMessagesContainerElem = new UIElement();
			this.ChildMessagesContainerElem.Top.Set( UIMessage.DefaultHeight - 8f, 0f );
			this.ChildMessagesContainerElem.Width.Set( 0f, 1f );
			this.ChildMessagesContainerElem.SetPadding( 8f );
			this.ChildMessagesContainerElem.PaddingRight = 0f;
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
	}
}

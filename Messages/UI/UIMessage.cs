using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Elements;
using ModLibsUI.Classes.UI.Theme;
using Messages.Definitions;
using Messages.Logic;


namespace Messages.UI {
	partial class UIMessage : UIThemedPanel {
		public const float DefaultHeight = 40f;


		public const float DefaultTitleScale = 1.1f;

		public const float DefaultDescScale = 0.8f;



		////////////////

		public static int GetMessageIndexInList( IList<UIElement> list, UIMessage element ) {
			int idx;
			int count = list.Count;

			for( idx = 0; idx < count; idx++ ) {
				var elem = list[idx] as UIMessage;
				if( elem.CompareTo( element ) > 0 ) {
					break;
				}
			}

			return idx;
		}



		////////////////

		private UIThemedText TitleElem;

		private UIThemedText DescriptionElem;

		private UIElement DescriptionContainerElem;

		private UIElement NestedMessagesElem;

		////

		private IList<UIMessage> NestedMessages = new List<UIMessage>();

		////

		private float DescriptionHeight;


		////////////////

		public Message Message { get; private set; }

		public bool IsOpen { get; private set; } = false;


		////////////////

		public event Action OnOpen;



		////////////////

		public UIMessage( Message message ) : base( UITheme.Vanilla, false ) {
			this.Message = message;

			this.Width.Set( 0f, 1f );
			this.Height.Set( UIMessage.DefaultHeight, 0f );
			
			this.OnClick += (UIMouseEvent evt, UIElement listeningElement) => this.ToggleOpen();

			//

			this.TitleElem = new UIThemedText( this.Theme, false, this.Message.Title, UIMessage.DefaultTitleScale );
			this.TitleElem.TextColor = Color.Yellow;
			this.TitleElem.Width.Set( 0f, 1f );
			this.Append( this.TitleElem );

			this.DescriptionContainerElem = new UIElement();
			this.DescriptionContainerElem.Top.Set( UIMessage.DefaultHeight, 0f );
			this.Append( this.DescriptionContainerElem );

			this.DescriptionElem = new UIThemedText( this.Theme, false, this.Message.Description, UIMessage.DefaultDescScale );
			this.DescriptionElem.TextColor = Color.White;
			this.DescriptionElem.Width.Set( 0f, 1f );
			//this.Append( this.DescriptionElem );

			this.NestedMessagesElem = new UIElement();
			this.NestedMessagesElem.Top.Set( UIMessage.DefaultHeight, 0f );
			this.Append( this.NestedMessagesElem );

			//

			this.DescriptionHeight = Main.fontMouseText.MeasureString( this.Message.Description ).Y;
		}


		////////////////
		
		public float CalculateInnerHeight( bool open ) {
			if( open ) {
				return UIMessage.DefaultHeight + (this.DescriptionHeight * UIMessage.DefaultDescScale) + 8f;
			}
			return UIMessage.DefaultHeight;
		}
		
		public float CalculateNestedMessagesHeight() {
			float height = 0f;

			foreach( UIMessage elem in this.NestedMessages ) {
				height += elem.CalculateInnerHeight( elem.IsOpen ) + 8f;
			}

			return height;
		}


		////////////////
		
		public void AddNestedMessage( UIMessage messageElem ) {
			var list = this.NestedMessages
				.Select( e => (UIElement)e )
				.ToList();
			int idx = UIMessage.GetMessageIndexInList( list, messageElem );

			this.NestedMessages.Insert( idx, messageElem );

			//

			if( this.IsOpen ) {
				this.NestedMessagesElem.RemoveAllChildren();

				foreach( UIMessage msgElem in this.NestedMessages ) {
					this.NestedMessagesElem.Append( msgElem );
				}

				this.Recalculate();
			}
		}


		////////////////

		public override int CompareTo( object obj ) {
			var that = obj as UIMessage;
			if( that == null ) {
				return this.Message.Title.CompareTo( obj );
			}

			if( this.Message.Weight > that.Message.Weight ) {
				return 1;
			} else if( this.Message.Weight < that.Message.Weight ) {
				return -1;
			} else {
				return this.Message.Title.CompareTo( that.Message.Title );
			}
		}
	}
}

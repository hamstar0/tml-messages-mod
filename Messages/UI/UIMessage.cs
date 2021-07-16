using System;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Elements;
using ModLibsUI.Classes.UI.Theme;
using Messages.Definitions;


namespace Messages.UI {
	partial class UIMessage : UIThemedPanel {
		public const float DefaultHeight = 40f;


		public const float DefaultTitleScale = 1.1f;

		public const float DefaultDescScale = 0.8f;



		////////////////

		public static int GetMessageIndexInList( IList<UIMessage> list, UIMessage element ) {
			int idx;
			int count = list.Count;

			for( idx = 0; idx < count; idx++ ) {
				if( list[idx].CompareTo( element ) > 0 ) {
					break;
				}
			}

			return idx;
		}



		////////////////

		private UIThemedText TitleElem;

		private UIThemedText DescriptionElem;

		private UIElement DescriptionContainerElem;

		private UIElement ChildMessagesContainerElem;

		////

		private IList<UIMessage> ChildMessageElems = new List<UIMessage>();

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

			this.OnInitializeMe();
		}


		////////////////

		public override void Recalculate() {
			float height = this.CalculateInnerHeight( this.IsOpen );
			if( this.IsOpen ) {
				height += this.CalculateNestedMessagesHeight();
			}

			this.Height.Set( height, 0f );

			base.Recalculate();
		}


		////////////////

		public float CalculateInnerHeight( bool open ) {
			if( open ) {
				float height = UIMessage.DefaultHeight
					+ (this.DescriptionHeight * UIMessage.DefaultDescScale)
					+ 8f;
//LogLibraries.LogOnce( "inner height (open) for "+this.Message.ID+": "+height );
				return height;
			}
			return UIMessage.DefaultHeight;
		}
		
		public float CalculateNestedMessagesHeight() {
			float height = 0f;

			foreach( UIMessage elem in this.ChildMessageElems ) {
				float childHeight = elem.CalculateInnerHeight( elem.IsOpen );
				if( elem.IsOpen ) {
					childHeight += elem.CalculateNestedMessagesHeight();
				}

				height += childHeight;
				height += 8f;
//LogLibraries.LogOnce( "nested height (open) for "+this.Message.ID+" child "+elem.Message.ID+": "+childHeight+" ("+height+")" );
			}
			
			if( this.ChildMessageElems.Count >= 1 ) {
				height += 16f;
			}

			return height;
		}


		////////////////
		
		public int AddChildMessageElem( UIMessage messageElem ) {
			int idx = UIMessage.GetMessageIndexInList( this.ChildMessageElems, messageElem );

			this.ChildMessageElems.Insert( idx, messageElem );
//LogLibraries.Log( "AddChildMessageElem current:" + this.Message.ID + " ("+this.GetHashCode()+")"
//	+ ", child: " + messageElem.Message.ID
//	+ ", idx: " + idx
//	+ ", children: "+this.ChildMessageElems.Count );

			//

			if( this.IsOpen ) {
				this.Close( false );
				this.Open( false );
			}

			return idx;
		}


		////////////////

		public override int CompareTo( object obj ) {
			var that = obj as UIMessage;
			if( that == null ) {
				return this.Message.CompareTo( obj );
			}

			return this.Message.CompareTo( that.Message );
		}
	}
}

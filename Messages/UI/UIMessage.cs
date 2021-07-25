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
		public const float DefaultHeight = 36f;



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

		private UIThemedText TreeIconElem;
		
		private UIThemedText UnreadTextElem;

		private UIThemedText TitleElem;

		private UIThemedText DescriptionElem;

		private UIElement InfoContainer;

		private UIElement ChildMessagesContainerElem;

		private UIMessagesTab TabContainer;

		////

		private IList<UIMessage> ChildMessageElems = new List<UIMessage>();

		private ISet<string> UnreadHere = new HashSet<string>();


		////////////////

		public Message Message { get; private set; }

		public bool IsOpen { get; private set; } = false;


		////////////////

		public event Action OnOpen;



		////////////////

		public UIMessage( Message message, UIMessagesTab tabContainer )
					: base( UITheme.Vanilla, false ) {
			this.Message = message;
			this.TabContainer = tabContainer;

			this.OnInitializeMe();
		}


		////////////////
		
		public int AddChildMessageElem( UIMessage messageElem ) {
			int idx = UIMessage.GetMessageIndexInList( this.ChildMessageElems, messageElem );

			this.ChildMessageElems.Insert( idx, messageElem );

			//

			if( this.IsOpen ) {
				this.Close( false );
				this.Open( true, false );
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

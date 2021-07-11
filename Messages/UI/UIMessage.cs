using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using ModLibsUI.Classes.UI.Elements;
using ModLibsUI.Classes.UI.Theme;
using ModLibsCore.Libraries.Debug;
using Messages.Definitions;
using Messages.Logic;


namespace Messages.UI {
	partial class UIMessage : UIThemedPanel {
		public const float DefaultHeight = 40f;


		public const float DefaultTitleScale = 1.1f;

		public const float DefaultDescScale = 0.8f;



		////////////////

		private UIThemedText TitleElem;
		private UIThemedText DescriptionElem;

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

			this.DescriptionElem = new UIThemedText( this.Theme, false, this.Message.Description, UIMessage.DefaultDescScale );
			this.DescriptionElem.TextColor = Color.White;
			this.DescriptionElem.Top.Set( UIMessage.DefaultHeight, 0f );
			this.DescriptionElem.Width.Set( 0f, 1f );
			//this.Append( this.DescriptionElem );

			//

			this.DescriptionHeight = Main.fontMouseText.MeasureString( this.Message.Description ).Y;
		}


		////////////////
		
		public void ToggleOpen() {
			if( this.IsOpen ) {
				this.Close( true );
			} else {
				this.Open( true );
			}
		}

		////

		internal void Open( bool viaInterface ) {
			this.IsOpen = true;
			this.Message.IsRead = true;

			this.Append( this.DescriptionElem );

			if( viaInterface ) {
				this.OnOpen?.Invoke();
			}

			this.Recalculate();

			float addedHeight = (this.DescriptionHeight * UIMessage.DefaultDescScale) + 8f;

			this.Height.Set( UIMessage.DefaultHeight + addedHeight, 0f );
		}

		internal void Close( bool viaInterface ) {
			this.IsOpen = false;

			this.RemoveChild( this.DescriptionElem );

			this.Recalculate();

			this.Height.Set( UIMessage.DefaultHeight, 0f );
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


		////////////////

		public override void Update( GameTime gameTime ) {
			var mngr = ModContent.GetInstance<MessageManager>();
			if( !mngr.CurrentMessages.ContainsKey(this.Message.Title) ) {
				return;
			}

			if( mngr.CurrentMessages[ this.Message.Title ].IsRead ) {
				if( MessagesMod.Instance.MessagesTabUI?.RecentMessage != this ) {
					this.TitleElem.TextColor = Color.Gray;
					this.DescriptionElem.TextColor = Color.Gray;
				}
			} else {
				this.TitleElem.TextColor = Color.Yellow;
				this.DescriptionElem.TextColor = Color.White;
			}
		}
	}
}

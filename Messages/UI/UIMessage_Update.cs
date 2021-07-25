using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Elements;
using Messages.Logic;
using Messages.Definitions;


namespace Messages.UI {
	partial class UIMessage : UIThemedPanel {
		public override void Update( GameTime gameTime ) {
			var mngr = ModContent.GetInstance<MessageManager>();
			if( !mngr.MessagesByID.ContainsKey(this.Message.ID) ) {
				return;
			}

			if( this.IsOpen ) {
				this.TreeIconElem.SetText( "-" );
			} else {
				this.TreeIconElem.SetText( "+" );
			}

			if( this.InfoContainer.IsMouseHovering ) {
				this.TitleElem.TextColor *= 1.5f;
			}

			//

			Message msg = mngr.MessagesByID[ this.Message.ID ];
			var myplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );
			bool isUnread = myplayer.IsMessageRead( msg.ID );

			if( isUnread ) {
				this.UpdateForUnreadState();
			} else {
				this.UpdateForReadState();
			}

			//

			this.UnreadHere = this.Message.GetUnreadChildren();

			int unreadCount = this.UnreadHere.Count;

			if( unreadCount >= 1 ) {
				this.UnreadTextElem.TextColor = Color.Yellow;
				this.UnreadTextElem.SetText( ""+unreadCount );
			} else {
				this.UnreadTextElem.SetText( "" );
			}

			//

			base.Update( gameTime );
		}


		////

		private void UpdateForUnreadState() {
			var mngr = ModContent.GetInstance<MessageManager>();

			if( mngr.MessagesTabUI.RecentMessageID == this.Message.ID ) {
				this.TitleElem.TextColor = Color.White;
				this.DescriptionElem.TextColor = Color.White;
			} else {
				this.TitleElem.TextColor = Color.Gray;
				this.DescriptionElem.TextColor = Color.Gray;
			}
		}

		private void UpdateForReadState() {
			this.TitleElem.TextColor = Color.Yellow;
			this.DescriptionElem.TextColor = Color.White;

			//

			string id = this.Message.ID;

			for( UIMessage curr = this; curr != null; curr = curr.Parent?.Parent as UIMessage ) {
				curr.UnreadHere.Remove( id );
			}
		}
	}
}

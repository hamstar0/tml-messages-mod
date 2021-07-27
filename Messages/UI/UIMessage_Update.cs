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

			this.UpdateTreeIcon();
			this.UpdateForState();
			this.UpdateUnreadIcon();

			if( this.InfoContainer.IsMouseHovering ) {
				this.TitleElem.TextColor *= 1.5f;
			}

			//

			base.Update( gameTime );
		}


		////////////////

		private void UpdateTreeIcon() {
			if( this.ChildMessageElems.Count >= 1 ) {
				if( this.IsTreeExpanded ) {
					this.TreeIconElem.SetText( "-" );
				} else {
					this.TreeIconElem.SetText( "+" );
				}
			} else {
				this.TreeIconElem.SetText( "" );
			}
		}

		private void UpdateUnreadIcon() {
			this.UnreadHere = this.Message.GetUnreadChildren(true);

			int unreadCount = this.UnreadHere.Count;

			if( unreadCount >= 1 ) {
				this.UnreadTextElem.TextColor = Color.Yellow;
				this.UnreadTextElem.SetText( "" + unreadCount );
			} else {
				this.UnreadTextElem.SetText( "" );
			}
		}

		private void UpdateForState() {
			var mngr = ModContent.GetInstance<MessageManager>();
			Message msg = mngr.MessagesByID[ this.Message.ID ];

			var myplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );
			bool isUnread = myplayer.IsMessageRead( msg.ID );

			if( isUnread ) {
				this.UpdateForUnreadState();
			} else {
				this.UpdateForReadState();
			}
		}


		////////////////

		private void UpdateForUnreadState() {
			var mngr = ModContent.GetInstance<MessageManager>();

			if( mngr.MessagesTabUI.RecentMessageID == this.Message.ID ) {
				this.TitleElem.TextColor = Color.White;
			} else {
				this.TitleElem.TextColor = Color.Gray;
			}
		}

		private void UpdateForReadState() {
			this.TitleElem.TextColor = Color.Yellow;

			//

			string id = this.Message.ID;

			for( UIMessage curr = this; curr != null; curr = curr.Parent?.Parent as UIMessage ) {
				curr.UnreadHere.Remove( id );
			}
		}
	}
}

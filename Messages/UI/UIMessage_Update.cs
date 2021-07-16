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

			Message msg = mngr.MessagesByID[ this.Message.ID ];
			var myplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );

			if( myplayer.IsMessageRead(msg.ID) ) {
				if( mngr.MessagesTabUI != null && mngr.MessagesTabUI.RecentMessage != this ) {
					this.TitleElem.TextColor = Color.Gray;
					this.DescriptionElem.TextColor = Color.Gray;
				}
			} else {
				this.TitleElem.TextColor = Color.Yellow;
				this.DescriptionElem.TextColor = Color.White;
			}

			base.Update( gameTime );
		}
	}
}

using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Elements;
using Messages.Logic;


namespace Messages.UI {
	partial class UIMessage : UIThemedPanel {
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

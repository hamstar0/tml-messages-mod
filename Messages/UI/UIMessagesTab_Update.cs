using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using ModControlPanel.Internals.ControlPanel;
using Messages.Logic;


namespace Messages.UI {
	partial class UIMessagesTab : UIControlPanelTab {
		public override void Update( GameTime gameTime ) {
			base.Update( gameTime );

			ISet<string> unreadMsgIds = ModContent.GetInstance<MessageManager>().GetUnreadMessages();

			if( unreadMsgIds.Count > 0 ) {
				this.UpdateForUnreadMessages();
			} else {
				this.UpdateForNoUnreadMessages();
			}

			string recentMsgId = this.RecentMessage?.Message.ID;
			var myplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );

			if( recentMsgId != null && myplayer.IsMessageRead( recentMsgId ) ) {
				if( !this.ThisUnreadButton.IsInteractive ) {
					this.ThisUnreadButton.Enable();
				}
			} else {
				if( this.ThisUnreadButton.IsInteractive ) {
					this.ThisUnreadButton.Disable();
				}
			}
		}


		////////////////
		
		private void UpdateForUnreadMessages() {
			if( !this.GetUnreadButton.IsInteractive ) {
				this.GetUnreadButton.Enable();
				this.AllReadButton.Enable();
			}
		}

		private void UpdateForNoUnreadMessages() {
			if( this.GetUnreadButton.IsInteractive ) {
				this.GetUnreadButton.Disable();
				this.AllReadButton.Disable();
			}
		}
	}
}

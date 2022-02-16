using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using ModUtilityPanels.Classes.UI;
using Messages.Logic;


namespace Messages.UI {
	partial class UIMessagesTab : UIUtilityPanelsTab {
		public override void Update( GameTime gameTime ) {
			base.Update( gameTime );

			ISet<string> unreadMsgIds = ModContent.GetInstance<MessageManager>().GetUnreadMessages(out _);

			if( unreadMsgIds.Count >= 1 ) {
				this.UpdateForUnreadMessages();
			} else {
				this.UpdateForNoUnreadMessages();
			}

			string recentMsgId = this.RecentMessageID;
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


		////////////////

		private void UpdateTreeScrollbar() {
			bool listChanged;

			this.MessagesTreeListScrollbar.IsHidden = UIHideableScrollbar.IsScrollbarHidden(
				(int)this.MessagesTreeList.Height.Pixels,
				this.MessagesTreeList.Parent
			);

			if( this.MessagesTreeListScrollbar.IsHidden ) {
				listChanged = this.MessagesTreeList.Width.Pixels != 0f;
				this.MessagesTreeList.Width.Pixels = 0f;
			} else {
				listChanged = this.MessagesTreeList.Width.Pixels != -25f;
				this.MessagesTreeList.Width.Pixels = -25f;
			}

			if( listChanged ) {
				this.Recalculate();
				this.MessagesTreeList.Recalculate();
			}
		}

		private void UpdateBodyScrollbar() {
			bool listChanged;

			this.MessageBodyListScrollbar.IsHidden = UIHideableScrollbar.IsScrollbarHidden(
				(int)this.MessageBodyList.Height.Pixels,
				this.MessageBodyList.Parent
			);

			if( this.MessageBodyListScrollbar.IsHidden ) {
				listChanged = this.MessageBodyList.Width.Pixels != 0f;
				this.MessageBodyList.Width.Pixels = 0f;
			} else {
				listChanged = this.MessageBodyList.Width.Pixels != -25f;
				this.MessageBodyList.Width.Pixels = -25f;
			}

			if( listChanged ) {
				this.Recalculate();
				this.MessageBodyList.Recalculate();
			}
		}
	}
}

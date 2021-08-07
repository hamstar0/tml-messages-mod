using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.ModLoader;
using ModControlPanel.Services.UI.ControlPanel;
using Messages.Logic;


namespace Messages {
	public partial class MessagesMod : Mod {
		public void ShowAlert() {
			this.AlertTickDuration = 60 * 15;
		}

		public void HideAlert() {
			this.AlertTickDuration = 0;
		}


		////////////////

		public override void PostDrawInterface( SpriteBatch spriteBatch ) {
			if( Main.gameMenu ) {
				return;
			}

			if( this.AlertTickDuration >= 1 ) {
				this.AlertTickDuration--;

				this.DrawMessageAlertIf( spriteBatch );
			}
		}

		private void DrawMessageAlertIf( SpriteBatch spriteBatch ) {
			var mngr = ModContent.GetInstance<MessageManager>();
			int messageCount = mngr.GetUnreadMessages().Count;
			if( messageCount == 0 ) {
				this.AlertTickDuration = 0;

				return;
			}

			Rectangle buttonArea = this.DrawMessageAlert( spriteBatch, messageCount );

			if( buttonArea.Contains( Main.mouseX, Main.mouseY ) ) {
				Main.LocalPlayer.mouseInterface = true;

				if( Main.mouseLeft && Main.mouseLeftRelease ) {
					this.AlertTickDuration = 0;

					ControlPanelTabs.OpenTab( MessagesMod.ControlPanelTabName );

					ModContent.GetInstance<MessageManager>()
						.MessagesTabUI
						.OpenNextUnreadMessage();
				}
			}
		}


		////////////////

		private Rectangle DrawMessageAlert( SpriteBatch sb, int messageCount ) {
			var pos = new Vector2(
				Main.screenWidth / 2,
				(4 * Main.screenHeight) / 5
			);
			var texOrigin = new Vector2(
				this.MessageAlertTex.Width / 2,
				this.MessageAlertTex.Height / 2
			);

			float pulse = (float)Main.mouseTextColor / 255f;
			//pulse = 0.8f + 0.2f * pulse;
			pulse = pulse * pulse * pulse * pulse;

			float scaleBase = 1f;   //pulse
			float texScale = scaleBase * 0.25f;

			sb.Draw(
				texture: this.MessageAlertTex,
				position: pos,
				sourceRectangle: null,
				color: Color.White * 0.6f * pulse,
				rotation: 0f,
				origin: texOrigin,
				scale: texScale,
				effects: SpriteEffects.None,
				layerDepth: 0f
			);

			string msgText = "" + messageCount;
			float msgScale = scaleBase * 1.5f;
			Vector2 msgDim = Main.fontMouseText.MeasureString( msgText );
			Vector2 msgOrigin = msgDim * 0.5f;

			sb.DrawString(
				spriteFont: Main.fontMouseText,
				text: msgText,
				position: pos + new Vector2(0f, 16f),
				color: Color.Cyan * 0.6f * pulse,
				rotation: 0f,
				origin: msgOrigin,
				scale: msgScale,
				effects: SpriteEffects.None,
				layerDepth: 0f
			);

			return new Rectangle(
				(int)(pos.X - (texOrigin.X * texScale)),
				(int)(pos.Y - (texOrigin.Y * texScale)),
				(int)(texScale * (float)this.MessageAlertTex.Width),
				(int)(texScale * (float)this.MessageAlertTex.Height)
			);
		}
	}
}
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.ModLoader;
using ModUtilityPanels.Services.UI.UtilityPanels;
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


		////////////////

		private void DrawMessageAlertIf( SpriteBatch spriteBatch ) {
			var mngr = ModContent.GetInstance<MessageManager>();
			int messageCount = mngr.GetUnreadMessages( out ISet<string> important )
				.Count;
			if( messageCount == 0 ) {
				this.AlertTickDuration = 0;

				return;
			}

			Rectangle buttonArea = this.DrawMessageAlert( spriteBatch, messageCount, important.Count >= 1 );

			if( buttonArea.Contains( Main.mouseX, Main.mouseY ) ) {
				Main.LocalPlayer.mouseInterface = true;

				if( Main.mouseLeft && Main.mouseLeftRelease ) {
					this.AlertTickDuration = 0;

					UtilityPanelsTabs.OpenTab( MessagesMod.UtilityPanelsTabName );

					ModContent.GetInstance<MessageManager>()
						.MessagesTabUI
						.OpenNextUnreadMessage();
				}
			}
		}


		////

		 private int ImportantMsgAnimCycle = 0;

		private Rectangle DrawMessageAlert( SpriteBatch sb, int messageCount, bool hasImportance ) {
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

			Texture2D tex = hasImportance
				? this.MessageAlert2Tex
				: this.MessageAlertTex;

			//

			sb.Draw(
				texture: tex,
				position: pos,
				sourceRectangle: null,
				color: Color.White * 0.6f * pulse,
				rotation: 0f,
				origin: texOrigin,
				scale: texScale,
				effects: SpriteEffects.None,
				layerDepth: 0f
			);

			if( hasImportance ) {
				if( this.ImportantMsgAnimCycle++ > 30 ) {
					this.ImportantMsgAnimCycle = 0;
				}
				float animCycle = (float)this.ImportantMsgAnimCycle / 30f;
				float importantAddedScale = animCycle / 4f;

				sb.Draw(
					texture: tex,
					position: pos,
					sourceRectangle: null,
					color: Color.White * 0.6f * (1f - animCycle),
					rotation: 0f,
					origin: texOrigin,
					scale: texScale + importantAddedScale,
					effects: SpriteEffects.None,
					layerDepth: 0f
				);
			}

			//

			this.DrawMessageAlertCount(
				sb: sb,
				pos: pos,
				scaleBase: scaleBase,
				pulse: pulse,
				messageCount: messageCount,
				hasImportance: hasImportance
			);

			//

			return new Rectangle(
				(int)(pos.X - (texOrigin.X * texScale)),
				(int)(pos.Y - (texOrigin.Y * texScale)),
				(int)(texScale * (float)this.MessageAlertTex.Width),
				(int)(texScale * (float)this.MessageAlertTex.Height)
			);
		}

		////

		private void DrawMessageAlertCount(
					SpriteBatch sb,
					Vector2 pos,
					float scaleBase,
					float pulse,
					int messageCount,
					bool hasImportance ) {
			string msgText = "" + messageCount;
			float msgScale = scaleBase * 1.5f;
			Vector2 msgDim = Main.fontMouseText.MeasureString( msgText );
			Vector2 msgOrigin = msgDim * 0.5f;
			Color color = hasImportance
				? Color.Yellow
				: Color.Cyan;

			sb.DrawString(
				spriteFont: Main.fontMouseText,
				text: msgText,
				position: pos + new Vector2(0f, 16f),
				color: color * 0.6f * pulse,
				rotation: 0f,
				origin: msgOrigin,
				scale: msgScale,
				effects: SpriteEffects.None,
				layerDepth: 0f
			);
		}
	}
}
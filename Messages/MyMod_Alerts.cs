using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using ModControlPanel.Services.UI.ControlPanel;


namespace Messages {
	public partial class MessagesMod : Mod {
		public void ShowAlert() {
			this.AlertTickDuration = 60 * 5;
		}


		////////////////

		public override void PostDrawInterface( SpriteBatch spriteBatch ) {
			if( this.AlertTickDuration >= 1 ) {
				Rectangle buttonArea = this.DrawMessageAlert( spriteBatch );

				if( Main.mouseLeft && Main.mouseLeftRelease && buttonArea.Contains(Main.mouseX, Main.mouseY) ) {
					this.AlertTickDuration = 0;

					ControlPanelTabs.OpenTab( MessagesMod.ControlPanelName );
				}
			}
		}


		////////////////

		private Rectangle DrawMessageAlert( SpriteBatch sb ) {
			var pos = new Vector2(
				Main.screenWidth / 2,
				(4 * Main.screenHeight) / 5
			);
			var origin = new Vector2(
				this.MessageAlertTex.Width / 2,
				this.MessageAlertTex.Height / 2
			);

			float pulse = (float)Main.mouseTextColor / 255f;
			pulse = 0.8f + 0.2f * pulse;

			float scale = 0.25f;
			scale *= pulse;

			sb.Draw(
				texture: this.MessageAlertTex,
				position: pos,
				sourceRectangle: null,
				color: Color.White * 0.5f,
				rotation: 0f,
				origin: origin,
				scale: scale,
				effects: SpriteEffects.None,
				layerDepth: 0
			);

			return new Rectangle(
				(int)(pos.X - (origin.X * scale)),
				(int)(pos.Y - (origin.Y * scale)),
				(int)(scale * (float)this.MessageAlertTex.Width),
				(int)(scale * (float)this.MessageAlertTex.Height)
			);
		}
	}
}
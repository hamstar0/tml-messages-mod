using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Messages {
	public partial class MessagesMod : Mod {
		public const string ControlPanelName = "Messages";


		////

		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-messages-mod";


		////////////////

		public static MessagesMod Instance => ModContent.GetInstance<MessagesMod>();



		////////////////

		public ModHotKey ControlPanelHotkey { get; private set; }


		////////////////

		private int AlertTickDuration = 0;

		private Texture2D MessageAlertTex;



		////////////////
		
		public override void Load() {
			this.ControlPanelHotkey = this.RegisterHotKey( "Toggle Messages", "OemTilde" );

			if( !Main.dedServ && Main.netMode != NetmodeID.Server ) {
				this.MessageAlertTex = this.GetTexture( "UI/MessageAlert" );
			}
		}
	}
}
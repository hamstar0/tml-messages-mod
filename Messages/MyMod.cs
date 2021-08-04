using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.TModLoader.Mods;


namespace Messages {
	public partial class MessagesMod : Mod {
		public const string ControlPanelTabName = "Messages";


		////////////////

		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-messages-mod";


		////////////////

		public static MessagesMod Instance => ModContent.GetInstance<MessagesMod>();



		////////////////

		internal event Action OnMessageTabInitialize;

		internal event Action OnMessageTabCategoriesInitialize;



		////////////////

		private int AlertTickDuration = 0;

		private Texture2D MessageAlertTex;


		////////////////

		public bool IsMessageTabInitialized { get; private set; } = false;

		public bool IsMessageTabCategoriesInitialized { get; private set; } = false;

		////

		public ModHotKey ControlPanelHotkey { get; private set; }



		////////////////
		
		public override void Load() {
			this.ControlPanelHotkey = this.RegisterHotKey( "Toggle Messages", "OemTilde" );

			if( !Main.dedServ && Main.netMode != NetmodeID.Server ) {
				this.MessageAlertTex = this.GetTexture( "UI/MessageAlert" );
			}
		}


		////////////////

		public override object Call( params object[] args ) {
			return ModBoilerplateLibraries.HandleModCall( typeof(MessagesAPI), args );
		}


		////////////////

		internal void RunMessagesInitializeEvent() {
			this.IsMessageTabInitialized = true;
			this.OnMessageTabInitialize?.Invoke();
		}
		
		internal void RunMessageCategoriesInitializeEvent() {
			this.IsMessageTabCategoriesInitialized = true;
			this.OnMessageTabCategoriesInitialize?.Invoke();
		}
	}
}
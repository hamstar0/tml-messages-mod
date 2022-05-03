using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.TModLoader.Mods;


namespace Messages {
	public partial class MessagesMod : Mod {
		public const string UtilityPanelsTabName = "Messages";


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
		private Texture2D MessageAlert2Tex;


		////////////////

		public bool IsMessageTabInitialized { get; private set; } = false;

		public bool IsMessageTabCategoriesInitialized { get; private set; } = false;

		////

		public ModHotKey UtilPanelHotkey { get; private set; }



		////////////////
		
		public override void Load() {
			this.UtilPanelHotkey = this.RegisterHotKey( "Toggle Messages", "OemTilde" );

			if( !Main.dedServ && Main.netMode != NetmodeID.Server ) {
				this.MessageAlertTex = this.GetTexture( "UI/MessageAlert" );
				this.MessageAlert2Tex = this.GetTexture( "UI/MessageAlert2" );
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
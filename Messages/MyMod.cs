using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsUI.Classes.UI.Theme;
using ModControlPanel.Services.UI.ControlPanel;
using Messages.UI;
using Messages.Logic;


namespace Messages {
	public class MessagesMod : Mod {
		public const string ControlPanelName = "Messages";


		////

		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-messages-mod";


		////////////////

		public static MessagesMod Instance => ModContent.GetInstance<MessagesMod>();



		////////////////

		public ModHotKey ControlPanelHotkey { get; private set; }


		////////////////

		internal UIMessagesTab MessagesTabUI;



		////////////////

		public override void Load() {
			this.ControlPanelHotkey = this.RegisterHotKey( "Toggle Messages", "OemTilde" );
		}

		public override void PostSetupContent() {
			if( !Main.dedServ && Main.netMode != NetmodeID.Server ) {
				// Add player stats tab
				this.MessagesTabUI = new UIMessagesTab( UITheme.Vanilla );
				ControlPanelTabs.AddTab( MessagesMod.ControlPanelName, this.MessagesTabUI );
			}
		}


		////////////////

		public override void PostUpdateEverything() {
			ModContent.GetInstance<MessageManager>()?.Update_Internal();
		}
	}
}
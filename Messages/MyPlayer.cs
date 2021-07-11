using System;
using Terraria.GameInput;
using Terraria.ModLoader;
using ModControlPanel.Services.UI.ControlPanel;


namespace Messages {
	partial class MessagesPlayer : ModPlayer {
		public override void ProcessTriggers( TriggersSet triggersSet ) {
			var mymod = (MessagesMod)this.mod;

			try {
				if( mymod.ControlPanelHotkey != null && mymod.ControlPanelHotkey.JustPressed ) {
					if( ControlPanelTabs.IsDialogOpen() ) {
						ControlPanelTabs.CloseDialog();
					} else {
						ControlPanelTabs.OpenTab( MessagesMod.ControlPanelName );
					}
				}
			} catch { }
		}
	}
}

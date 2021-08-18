using System;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using ModUtilityPanels.Services.UI.UtilityPanels;


namespace Messages {
	partial class MessagesPlayer : ModPlayer {
		public override void ProcessTriggers( TriggersSet triggersSet ) {
			var mymod = (MessagesMod)this.mod;

			try {
				if( mymod.UtilPanelHotkey?.JustPressed == true ) {
					if( UtilityPanelsTabs.IsDialogOpen() ) {
						UtilityPanelsTabs.CloseDialog();
					} else {
						UtilityPanelsTabs.OpenTab( MessagesMod.UtilityPanelsTabName );
					}
				}
			} catch { }
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using ModControlPanel.Internals.ControlPanel;


namespace Messages.UI {
	partial class UIMessagesTab : UIControlPanelTab {
		public ISet<string> GetUnreadMessages() {
			var mycustomplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );
			ISet<string> readMsgIds = mycustomplayer.GetReadMessageIdsForCurrentWorld();

			var unreadMsgIds = new HashSet<string>(
				this.MessageElems.Keys
					.Where( id=>!readMsgIds.Contains(id) )
			);

			return unreadMsgIds;
		}
	}
}

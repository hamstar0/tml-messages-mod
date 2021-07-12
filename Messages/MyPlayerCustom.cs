using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Terraria;
using Terraria.ID;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;


namespace Messages {
	partial class MessagesCustomPlayer : CustomPlayerData {
		private Dictionary<string, HashSet<string>> ReadMessagesByIdsPerWorld = new Dictionary<string, HashSet<string>>();



		////////////////

		protected override void OnEnter( bool isCurrentPlayer, object data ) {
			if( !isCurrentPlayer ) {
				return;
			}

			if( data != null ) {
//LogLibraries.Log( "ENTER "+string.Join(", ", this.CompletedObjectivesPerWorld.Select(kv=>kv.Key+":"+string.Join(",",kv.Value))) );
				this.ReadMessagesByIdsPerWorld = ((JObject)data)
					.ToObject<Dictionary<string, string[]>>()
					.ToDictionary( kv=>kv.Key, kv=>new HashSet<string>( kv.Value ) );
			}
//else { LogLibraries.Log( "ENTER!" ); }
		}


		protected override object OnExit() {
			var data = new Dictionary<string, HashSet<string>>( this.ReadMessagesByIdsPerWorld );

			if( Main.netMode != NetmodeID.Server ) {
				MessagesAPI.ClearMessages( false );
			}

//LogLibraries.Log( "EXIT "+string.Join(", ", data.Select(kv=>kv.Key+":"+string.Join(",",kv.Value))) );
			return data;
		}
	}
}

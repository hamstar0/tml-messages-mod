using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Terraria;
using Terraria.ID;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.World;


namespace Messages {
	partial class MessagesCustomPlayer : CustomPlayerData {
		private Dictionary<string, HashSet<string>> ReadMessagesPerWorld = new Dictionary<string, HashSet<string>>();



		////////////////

		protected override void OnEnter( bool isCurrentPlayer, object data ) {
			if( !isCurrentPlayer ) {
				return;
			}

			if( data != null ) {
//LogLibraries.Log( "ENTER "+string.Join(", ", this.CompletedObjectivesPerWorld.Select(kv=>kv.Key+":"+string.Join(",",kv.Value))) );
				this.ReadMessagesPerWorld = ((JObject)data)
					.ToObject<Dictionary<string, string[]>>()
					.ToDictionary( kv=>kv.Key, kv=>new HashSet<string>( kv.Value ) );
			}
//else { LogLibraries.Log( "ENTER!" ); }
		}

		protected override object OnExit() {
			var data = new Dictionary<string, HashSet<string>>( this.ReadMessagesPerWorld );

			if( Main.netMode != NetmodeID.Server ) {
				MessagesAPI.ClearMessages( false );
			}

			//LogLibraries.Log( "EXIT "+string.Join(", ", data.Select(kv=>kv.Key+":"+string.Join(",",kv.Value))) );
			return data;
		}


		////////////////

		public bool IsMessagesByNameRead( string messageTitle ) {
			string worldUid = WorldIdentityLibraries.GetUniqueIdForCurrentWorld( true );

			if( !this.ReadMessagesPerWorld.ContainsKey(worldUid) ) {
				return false;
			}
			return this.ReadMessagesPerWorld[ worldUid ].Contains( messageTitle );
		}


		public bool RecordReadMessage( string messageTitle ) {
			string worldUid = WorldIdentityLibraries.GetUniqueIdForCurrentWorld( true );

			if( !this.ReadMessagesPerWorld.ContainsKey( worldUid ) ) {
				this.ReadMessagesPerWorld[ worldUid ] = new HashSet<string>();
			}

			return this.ReadMessagesPerWorld[worldUid].Add( messageTitle );
//LogLibraries.Log( "RECORD "+worldUid+", "+objectiveTitle+", this: "+this.GetHashCode());
//LogLibraries.Log( "RECORD "+string.Join(", ", this.CompletedObjectivesPerWorld.Select(kv=>kv.Key+":"+string.Join(",",kv.Value))) );
		}

		
		public bool ForgetReadMessage( string messageTitle ) {
			string worldUid = WorldIdentityLibraries.GetUniqueIdForCurrentWorld( true );

			if( !this.ReadMessagesPerWorld.ContainsKey( worldUid ) ) {
				return false;
			}

			return this.ReadMessagesPerWorld[ worldUid ].Remove( messageTitle );
		}


		public void ClearReadMessages() {
//LogLibraries.Log( "CLEAR" );
			this.ReadMessagesPerWorld.Clear();
		}
	}
}

using System;
using System.Collections.Generic;
using Terraria;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.World;


namespace Messages {
	partial class MessagesCustomPlayer : CustomPlayerData {
		public bool IsMessageRead( string id ) {
			string worldUid = WorldIdentityLibraries.GetUniqueIdForCurrentWorld( true );

			if( !this.ReadMessagesByIdsPerWorld.ContainsKey(worldUid) ) {
				return false;
			}

			return this.ReadMessagesByIdsPerWorld[ worldUid ].Contains( id );
		}


		public ISet<string> GetReadMessageIdsForCurrentWorld() {
			string worldUid = WorldIdentityLibraries.GetUniqueIdForCurrentWorld( true );

			if( !this.ReadMessagesByIdsPerWorld.ContainsKey( worldUid ) ) {
				this.ReadMessagesByIdsPerWorld[ worldUid ] = new HashSet<string>();
			}

			return new HashSet<string>( this.ReadMessagesByIdsPerWorld[worldUid] );
		}

		
		public bool SetReadMessage( string id ) {
			string worldUid = WorldIdentityLibraries.GetUniqueIdForCurrentWorld( true );

			if( !this.ReadMessagesByIdsPerWorld.ContainsKey( worldUid ) ) {
				this.ReadMessagesByIdsPerWorld[ worldUid ] = new HashSet<string>();
			}

			return this.ReadMessagesByIdsPerWorld[worldUid].Add( id );
		}

		
		public bool UnsetReadMessage( string id ) {
			string worldUid = WorldIdentityLibraries.GetUniqueIdForCurrentWorld( true );

			if( !this.ReadMessagesByIdsPerWorld.ContainsKey( worldUid ) ) {
				return false;
			}

			return this.ReadMessagesByIdsPerWorld[ worldUid ].Remove( id );
		}


		public void UnsetAllReadMessagesForCurrentWorld() {
			string worldUid = WorldIdentityLibraries.GetUniqueIdForCurrentWorld( true );

			if( !this.ReadMessagesByIdsPerWorld.ContainsKey( worldUid ) ) {
				return;
			}

			this.ReadMessagesByIdsPerWorld[worldUid].Clear();
		}


		public void UnsetAllReadMessages() {
			this.ReadMessagesByIdsPerWorld.Clear();
		}
	}
}

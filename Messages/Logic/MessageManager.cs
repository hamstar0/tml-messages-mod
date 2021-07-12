using System;
using System.Collections.Concurrent;
using Terraria;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Classes.PlayerData;
using Messages.Definitions;


namespace Messages.Logic {
	partial class MessageManager : ILoadable {
		public ConcurrentDictionary<string, Message> CurrentMessages { get; } = new ConcurrentDictionary<string, Message>();



		////////////////

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnPostModsLoad() { }

		void ILoadable.OnModsUnload() { }


		////////////////

		public Message AddMessage( string title, string description, string id = null, int weight = 0, Message parent = null ) {
			if( this.CurrentMessages.ContainsKey(id) ) {
				return null;
			} else if( id == null && this.CurrentMessages.ContainsKey(title) ) {
				return null;
			}

			var msg = new Message( title, description, id, weight, parent );

			this.CurrentMessages[ msg.ID ] = msg;

			return msg;
		}

		public bool RemoveMessage( Message message, bool forceUnread = false ) {
			bool isRemoved = this.CurrentMessages.TryRemove( message.ID, out Message msg );

			if( forceUnread && isRemoved ) {
				var myplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.LocalPlayer.whoAmI );
				myplayer.ForgetReadMessage( msg.ID );
			}

			return isRemoved;
		}

		public void ClearAllMessages( bool forceUnread = false ) {
			if( forceUnread ) {
				var myplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.LocalPlayer.whoAmI );

				foreach( Message msg in this.CurrentMessages.Values ) {
					myplayer.ForgetReadMessage( msg.ID );
				}
			}

			this.CurrentMessages.Clear();
		}
	}
}

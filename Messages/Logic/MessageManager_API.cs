using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using ModControlPanel.Services.UI.ControlPanel;
using Messages.Definitions;


namespace Messages.Logic {
	partial class MessageManager : ILoadable {
		public static void AddAlert( bool forceUnread, Action<bool> onRun ) {
			
		}



		////////////////

		public bool AddMessage( Message message, bool alertPlayer, out string result ) {
			if( !this.AddMessageData( message, out result ) ) {
				return false;
			}

			var myplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );

			// Load data
			bool isPrevRead = myplayer?.IsMessagesByNameRead( message.Title ) ?? false;
			//message.Initialize( isPrevRead );

			MessagesMod.Instance.MessagesTabUI.AddMessage( message );	// Initializes message onto UI

			if( !isPrevRead && !message.IsRead && alertPlayer ) {
				MessageManager.AddAlert(
					forceUnread: true,
					onRun: ( isUnread ) => {
						if( isUnread ) {
							if( !ControlPanelTabs.IsDialogOpen() ) {
								ControlPanelTabs.OpenTab( MessagesMod.ControlPanelName );
							}
						}
					}
				);

				ControlPanelTabs.AddTabAlert( MessagesMod.ControlPanelName );

				//Main.NewText( "New objective added: " + message.Title, Color.Yellow );
				//
				//Main.PlaySound( SoundID.Chat, Main.LocalPlayer.MountedCenter );
			}

			//this.NotifySubscribers( message, true );

			return true;
		}


		public void RemoveMessage( string title, bool forceIncomplete ) {
			this.RemoveMessageData( title );

			if( forceIncomplete ) {
				var myplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );
				myplayer.ForgetReadMessage( title );
			}

			MessagesMod.Instance
				.MessagesTabUI
				.RemoveMessage( title );
		}


		public void ClearAllMessages( bool forceIncomplete ) {
			this.ClearMessagesData();

			if( forceIncomplete ) {
				var myplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );
				myplayer?.ClearReadMessages();
			}

			MessagesMod.Instance
				.MessagesTabUI
				.ClearMessages();
		}


		////

		/*public void NotifySubscribers( Message message, bool isNew ) {
			foreach( MessagesAPI.SubscriptionEvent evt in this.Subscribers.Values ) {
				bool isRead = message.isRead.HasValue
					? message.isRead.Value
					: false;
				evt.Invoke( message.Title, isNew, isRead );
			}
		}*/
	}
}

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Theme;
using ModUtilityPanels;
using ModUtilityPanels.Services.UI.UtilityPanels;
using Messages.Definitions;
using Messages.UI;


namespace Messages.Logic {
	partial class MessageManager : ILoadable {
		internal UIMessagesTab MessagesTabUI;


		////////////////

		public ConcurrentDictionary<string, Message> MessagesByID { get; } = new ConcurrentDictionary<string, Message>();

		////

		public Message ModInfoCategoryMsg { get; private set; }

		public Message HintsTipsCategoryMsg { get; private set; }

		public Message GameInfoCategoryMsg { get; private set; }

		public Message StoryLoreCategoryMsg { get; private set; }



		////////////////

		void ILoadable.OnModsLoad() {
			if( !Main.dedServ && Main.netMode != NetmodeID.Server ) {
				var utilPanelsMod = (ModUtilityPanelsMod)ModLoader.GetMod( "ModUtilityPanels" );

				utilPanelsMod.OnUtilityPanelsInitialize += () => {
					this.MessagesTabUI = new UIMessagesTab( UITheme.Vanilla );

					// Add tab
					UtilityPanelsTabs.AddTab( MessagesMod.ControlPanelTabName, this.MessagesTabUI );

					MessagesMod.Instance.RunMessagesInitializeEvent();
				};
			}
		}

		void ILoadable.OnPostModsLoad() { }

		void ILoadable.OnModsUnload() { }


		////////////////

		public ISet<string> GetUnreadMessages() {
			ISet<string> unreadMsgIds = null;

			try {
				var mycustomplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );
				ISet<string> readMsgIds = mycustomplayer.GetReadMessageIdsForCurrentWorld();

				unreadMsgIds = new HashSet<string>(
					this.MessagesByID
						.Keys
						.Where( id => !readMsgIds.Contains(id) )
				);
			} catch( Exception e ) {
				LogLibraries.Warn( e.ToString() );
			}

			return unreadMsgIds ?? new HashSet<string>();
		}


		////////////////

		public Message AddMessage(
					string title,
					string description,
					Mod modOfOrigin,
					out string result,
					string id = null,
					int weight = 0,
					Message parent = null ) {
			if( id != null ) {
				if( this.MessagesByID.ContainsKey(id) ) {
					result = "Message already exists by ID.";
					return null;
				}
			} else if( this.MessagesByID.ContainsKey(title) ) {
				result = "Message already exists by ID (title).";
				return null;
			}

			//

			var msg = new Message( title, description, modOfOrigin, id, weight );

			if( parent != null ) {
				parent.AddChild( msg );
			}

			this.MessagesByID[ msg.ID ] = msg;

			//

			this.MessagesTabUI.AddMessageAsElementInListIf( msg, parent );

			result = "Success.";
			return msg;
		}

		public bool RemoveMessage( Message message, bool forceUnread = false ) {
			bool isRemoved = this.MessagesByID.TryRemove( message.ID, out Message msg );

			if( forceUnread && isRemoved ) {
				var myplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.LocalPlayer.whoAmI );
				myplayer.UnsetReadMessage( msg.ID );
			}

			this.MessagesTabUI.RemoveMessageElementFromList( message );

			return isRemoved;
		}

		public void ClearAllMessages( bool forceUnread = false ) {
			if( forceUnread ) {
				var myplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.LocalPlayer.whoAmI );

				myplayer.UnsetAllReadMessagesForCurrentWorld();
			}

			this.MessagesByID.Clear();
			this.ModInfoCategoryMsg = null;
			this.HintsTipsCategoryMsg = null;
			this.GameInfoCategoryMsg = null;
			this.StoryLoreCategoryMsg = null;

			this.MessagesTabUI.ClearMessageElementsList();
		}
	}
}

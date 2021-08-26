using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;
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

		public ISet<string> ImportantMessagesByID { get; } = new HashSet<string>();

		////

		public Message ModInfoCategoryMsg { get; private set; }

		public Message HintsTipsCategoryMsg { get; private set; }

		public Message GameInfoCategoryMsg { get; private set; }

		public Message StoryLoreCategoryMsg { get; private set; }

		public Message EventsCategoryMsg { get; private set; }



		////////////////

		void ILoadable.OnModsLoad() {
			if( !Main.dedServ && Main.netMode != NetmodeID.Server ) {
				var utilPanelsMod = (ModUtilityPanelsMod)ModLoader.GetMod( "ModUtilityPanels" );

				utilPanelsMod.OnUtilityPanelsInitialize += () => {
					this.MessagesTabUI = new UIMessagesTab( UITheme.Vanilla );

					// Add tab
					UtilityPanelsTabs.AddTab( MessagesMod.UtilityPanelsTabName, this.MessagesTabUI );

					MessagesMod.Instance.RunMessagesInitializeEvent();
				};
			}
		}

		void ILoadable.OnPostModsLoad() { }

		void ILoadable.OnModsUnload() { }


		////////////////

		public ISet<string> GetUnreadMessages( out ISet<string> important ) {
			ISet<string> unreadMsgIds = null;
			important = null;

			try {
				var mycustomplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );
				ISet<string> readMsgIds = mycustomplayer.GetReadMessageIdsForCurrentWorld();

				unreadMsgIds = new HashSet<string>(
					this.MessagesByID
						.Keys
						.Where( id => !readMsgIds.Contains(id) )
				);
				important = new HashSet<string>(
					unreadMsgIds.Where( id => this.ImportantMessagesByID.Contains(id) )
				);
			} catch( Exception e ) {
				LogLibraries.Warn( e.ToString() );
			}

			return unreadMsgIds ?? new HashSet<string>();
		}


		////////////////

		public (Message, UIMessage) AddMessage(
					string title,
					string description,
					Mod modOfOrigin,
					bool isImportant,
					Message parent,
					out string result,
					string id = null,
					int weight = 0 ) {
			if( id != null ) {
				if( this.MessagesByID.ContainsKey(id) ) {
					result = "Message already exists by ID.";
					return (null, null);
				}
			}

			id = Message.GenerateMessageID( title, modOfOrigin );
			if( this.MessagesByID.ContainsKey(id) ) {
				result = "Message already exists by ID (message title + mod name).";
				return (null, null);
			}

			//

			var msg = new Message(
				title: title,
				description: description,
				modOfOrigin: modOfOrigin,
				id: id,
				weight: weight
			);

			if( parent != null ) {
				parent.AddChild( msg );
			}

			this.MessagesByID[ msg.ID ] = msg;

			if( isImportant ) {
				this.ImportantMessagesByID.Add( msg.ID );
			}

			//

			UIMessage msgElem = this.MessagesTabUI.AddMessageAsElementInListIf( msg, parent );

			result = "Success.";
			return (msg, msgElem);
		}

		////

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
			this.EventsCategoryMsg = null;

			this.MessagesTabUI.ClearMessageElementsList();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using Messages.Definitions;
using Messages.UI;


namespace Messages.Logic {
	partial class MessageManager : ILoadable {
		public ISet<string> GetUnreadMessages( out ISet<string> important ) {
			ISet<string> unreadMsgIds = null;

			try {
				var mycustomplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );
				if( mycustomplayer == null ) {
					important = new HashSet<string>();

					return new HashSet<string>();
				}

				//

				ISet<string> readMsgIds = mycustomplayer.GetReadMessageIdsForCurrentWorld();

				unreadMsgIds = new HashSet<string>(
					this.MessagesByID
						.Keys
						.Where( id => !readMsgIds.Contains(id) )
				);
//DebugLibraries.Print( "unread", string.Join(", ", unreadMsgIds) );

				//

				important = new HashSet<string>(
					unreadMsgIds.Where( id => this.ImportantMessagesByID.Contains(id) )
				);
			} catch( Exception e ) {
				important = new HashSet<string>();

				LogLibraries.Warn( e.ToString() );
			}

			//

			return unreadMsgIds ?? new HashSet<string>();
		}


		////////////////

		public (Message, UIMessage) AddMessage(
					string title,
					string description,
					Color? color,
					bool bigTitle,
					Mod modOfOrigin,
					bool isImportant,
					Message parent,
					out string result,
					string id = null,
					int weight = 0 ) {
			if( id != null ) {
				if( this.MessagesByID.ContainsKey(id) ) {
					result = $"Message already exists by ID ({id}).";
					return (null, null);
				}
			} else {
				id = Message.GenerateMessageID( title, modOfOrigin );

				//

				if( this.MessagesByID.ContainsKey(id) ) {
					result = $"Message already exists by genned ID ({id}).";
					return (null, null);
				}
			}

			//

			var msg = new Message(
				title: title,
				description: description,
				color: color,
				bigTitle: bigTitle,
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

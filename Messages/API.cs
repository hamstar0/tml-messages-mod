using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;
using ModControlPanel.Services.UI.ControlPanel;
using Messages.Definitions;
using Messages.Logic;


namespace Messages {
	/// <summary>
	/// Supplies API functions.
	/// </summary>
	public partial class MessagesAPI {
		/// <summary></summary>
		public static Message ModInfoCategoryMsg => ModContent.GetInstance<MessageManager>().ModInfoCategoryMsg;

		/// <summary></summary>
		public static Message HintsTipsCategoryMsg => ModContent.GetInstance<MessageManager>().HintsTipsCategoryMsg;

		/// <summary></summary>
		public static Message GameInfoCategoryMsg => ModContent.GetInstance<MessageManager>().GameInfoCategoryMsg;

		/// <summary></summary>
		public static Message StoryLoreCategoryMsg => ModContent.GetInstance<MessageManager>().StoryLoreCategoryMsg;



		////////////////

		/// <summary>
		/// Indicates if the current, local player has their messages loaded.
		/// </summary>
		/// <returns></returns>
		public static bool AreMessagesLoadedForCurrentPlayer() {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModLibsException( "Server has no player." );
			}

			if( Main.gameMenu ) {
				return false;
			}

			var myplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );
			return myplayer != null;
		}


		////////////////

		/// <summary></summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Message GetMessage( string id ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModLibsException( "Server Messages not allowed." );
			}

			var mngr = ModContent.GetInstance<MessageManager>();

			return mngr.MessagesByID.GetOrDefault( id );
		}

		/// <summary></summary>
		/// <returns></returns>
		public static ISet<string> GetUnreadMessageIDs() {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModLibsException( "Server Messages not allowed." );
			}

			var mngr = ModContent.GetInstance<MessageManager>();

			return mngr.GetUnreadMessages();
		}

		/// <summary></summary>
		/// <returns></returns>
		public static int GetUnreadMessageCount() {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModLibsException( "Server Messages not allowed." );
			}

			var mngr = ModContent.GetInstance<MessageManager>();

			return mngr.GetUnreadMessages().Count;
		}


		////////////////

		/// <summary>Adds a message to the list.</summary>
		/// <param name="title"></param>
		/// <param name="description"></param>
		/// <param name="modOfOrigin"></param>
		/// <param name="id">Allows for duplicate messages. Defaults to using `title` if null.</param>
		/// <param name="weight">Sort order priority of message in descending order.</param>
		/// <param name="parent">"Folder" message (`Message`) for the current message to belong to.</param>
		/// <param name="alertPlayer"></param>
		/// <returns>A non-null `Message` if message was registered successfully (i.e. no duplicates found).</returns>
		public static (Message msg, string result) AddMessage(
					string title,
					string description,
					Mod modOfOrigin,
					string id = null,
					int weight = 0,
					object parentMessage = null,
					bool alertPlayer = true ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModLibsException( "Server messages not allowed." );
			}
			if( !MessagesMod.Instance.IsMessageTabCategoriesInitialized ) {
				throw new ModLibsException( "Message display not finished initializing." );
			}
			if( parentMessage != null && !(parentMessage is Message) ) {
				throw new ModLibsException( "Invalid parentMessage." );
			}

			var mngr = ModContent.GetInstance<MessageManager>();
			
			Message msg = mngr.AddMessage(
				title: title,
				description: description,
				result: out string result,
				modOfOrigin: modOfOrigin,
				id: id,
				weight: weight,
				parent: parentMessage as Message
			);

			if( result == "Success." ) {
				if( alertPlayer ) {
					ControlPanelTabs.AddTabAlert( MessagesMod.ControlPanelTabName, false );

					MessagesMod.Instance.ShowAlert();
				}
			}

			return (msg, result);
		}

		////

		/// <summary></summary>
		/// <param name="message">A valid `Message` object.</param>
		/// <param name="forceIncomplete"></param>
		public static void RemoveMessage( object message, bool forceIncomplete ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModLibsException( "Server messages not allowed." );
			}
			if( !MessagesMod.Instance.IsMessageTabCategoriesInitialized ) {
				throw new ModLibsException( "Message display not finished initializing." );
			}
			if( !(message is Message) ) {
				throw new ModLibsException( "Invalid message object." );
			}

			var mngr = ModContent.GetInstance<MessageManager>();

			mngr.RemoveMessage( message as Message, forceIncomplete );
		}

		////

		/// <summary></summary>
		/// <param name="forceUnread"></param>
		public static void ClearMessages( bool forceUnread ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModLibsException( "Server messages not allowed." );
			}
			if( !MessagesMod.Instance.IsMessageTabCategoriesInitialized ) {
				throw new ModLibsException( "Message display not finished initializing." );
			}

			var mngr = ModContent.GetInstance<MessageManager>();

			mngr.ClearAllMessages( forceUnread );
		}
	}
}

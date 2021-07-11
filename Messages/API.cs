using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;
using Messages.Definitions;
using Messages.Logic;


namespace Messages {
	public partial class MessagesAPI {
		/// <summary>
		/// Indicates if the current, local player has their objectives loaded.
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
		/// <param name="title"></param>
		/// <returns></returns>
		public static Message GetMessage( string title ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModLibsException( "Server Messages not allowed." );
			}

			var mngr = ModContent.GetInstance<MessageManager>();

			return mngr.CurrentMessages.GetOrDefault( title );
		}


		////////////////

		/// <summary>Adds a message to the list.</summary>
		/// <param name="message"></param>
		/// <param name="alertPlayer">Creates an inbox message. Only alerts players for non-completed objectives.</param>
		/// <param name="result">Output message to indicate error type, or else `Success.`</param>
		/// <returns>`true` if objective isn't already defined and is being given a valid order index.</returns>
		public static bool AddMessage( Message message, bool alertPlayer, out string result ) {
			if( message == null ) {
				throw new ModLibsException( "Non-null message required." );
			}

			if( Main.netMode == NetmodeID.Server ) {
				throw new ModLibsException( "Server message not allowed." );
			}

			var mngr = ModContent.GetInstance<MessageManager>();

			return mngr.AddMessage( message, alertPlayer, out result );
		}

		////

		/// <summary></summary>
		/// <param name="title"></param>
		/// <param name="forceIncomplete"></param>
		public static void RemoveMessage( string title, bool forceIncomplete ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModLibsException( "Server objectives not allowed." );
			}

			var mngr = ModContent.GetInstance<MessageManager>();

			mngr.RemoveMessage( title, forceIncomplete );
		}

		////

		/// <summary></summary>
		/// <param name="forceIncomplete"></param>
		public static void ClearMessages( bool forceIncomplete ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModLibsException( "Server messages not allowed." );
			}

			var mngr = ModContent.GetInstance<MessageManager>();

			mngr.ClearAllMessages( forceIncomplete );
		}
	}
}

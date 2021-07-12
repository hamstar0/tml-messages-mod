﻿using System;
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
		/// <param name="title"></param>
		/// <param name="description"></param>
		/// <param name="id">Allows for duplicate messages. Defaults to using `title` if null.</param>
		/// <param name="alertPlayer"></param>
		/// <returns>A non-null `Message` if message was registered successfully (i.e. no duplicates found).</returns>
		public static Message AddMessage( string title, string description, string id = null, bool alertPlayer = true ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModLibsException( "Server messages not allowed." );
			}

			var mngr = ModContent.GetInstance<MessageManager>();

			Message msg = mngr.AddMessage( title, description, id );

			if( alertPlayer ) {
				f
			}

			return msg;
		}

		////

		/// <summary></summary>
		/// <param name="title"></param>
		/// <param name="forceIncomplete"></param>
		public static void RemoveMessage( Message message, bool forceIncomplete ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModLibsException( "Server messages not allowed." );
			}

			var mngr = ModContent.GetInstance<MessageManager>();

			mngr.RemoveMessage( message, forceIncomplete );
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

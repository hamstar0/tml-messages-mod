using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;
using Messages.Definitions;
using Messages.Logic;


namespace Messages {
	/// <summary>
	/// Supplies API functions.
	/// </summary>
	public partial class MessagesAPI {
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
		/// <param name="important"></param>
		/// <returns></returns>
		public static ISet<string> GetUnreadMessageIDs( out ISet<string> important ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModLibsException( "Server Messages not allowed." );
			}

			var mngr = ModContent.GetInstance<MessageManager>();

			return mngr.GetUnreadMessages( out important );
		}

		/// <summary></summary>
		/// <returns></returns>
		public static int GetUnreadMessageCount() {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModLibsException( "Server Messages not allowed." );
			}

			var mngr = ModContent.GetInstance<MessageManager>();

			return mngr.GetUnreadMessages(out _).Count;
		}
	}
}

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using Messages.Definitions;
using Messages.Logic;


namespace Messages {
	/// <summary>
	/// Supplies API functions.
	/// </summary>
	public partial class MessagesAPI {
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

			//

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

			//

			var mngr = ModContent.GetInstance<MessageManager>();

			mngr.ClearAllMessages( forceUnread );
		}
	}
}

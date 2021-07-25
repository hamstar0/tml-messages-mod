using System;
using Terraria;
using ModLibsCore.Libraries.Debug;


namespace Messages {
	public partial class MessagesAPI {
		/// <summary>
		/// Runs when the message database and UI are all setup.
		/// </summary>
		public static void AddMessagesInitializeEvent( Action func ) {
			if( MessagesMod.Instance.IsMessageTabInitialized ) {
				func();
			} else {
				MessagesMod.Instance.OnMessageTabInitialize += func;
			}
		}


		/// <summary>
		/// Runs when the message categories all setup.
		/// </summary>
		public static void AddMessagesCategoriesInitializeEvent( Action func ) {
			if( MessagesMod.Instance.IsMessageTabCategoriesInitialized ) {
				func();
			} else {
				MessagesMod.Instance.OnMessageTabCategoriesInitialize += func;
			}
		}
	}
}

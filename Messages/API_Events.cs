using System;
using Terraria;
using ModLibsCore.Libraries.Debug;


namespace Messages {
	public partial class MessagesAPI {
		/// <summary>
		/// Runs when the message database and UI are all setup.
		/// </summary>
		public static void AddMessagesInitializeEvent( Action func ) {
			var mymod = MessagesMod.Instance;

			if( mymod.IsMessageTabInitialized ) {
				func.Invoke();
			} else {
				mymod.OnMessageTabInitialize += func;
			}
		}


		/// <summary>
		/// Runs when the message categories all setup.
		/// </summary>
		public static void AddMessagesCategoriesInitializeEvent( Action func ) {
			var mymod = MessagesMod.Instance;

			if( mymod.IsMessageTabCategoriesInitialized ) {
				func.Invoke();
			} else {
				mymod.OnMessageTabCategoriesInitialize += func;
			}
		}
	}
}

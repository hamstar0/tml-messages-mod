using System;
using Terraria;
using ModLibsCore.Libraries.Debug;


namespace Messages {
	public partial class MessagesAPI {
		/// <summary>
		/// Runs when the message database and UI are all setup.
		/// </summary>
		public static void AddMessagesInitializeEvent( Action func ) {
			MessagesMod.Instance.OnInitialize += func;
		}


		/// <summary>
		/// Runs when the message categories all setup.
		/// </summary>
		public static void AddMessagesCategoriesInitializeEvent( Action func ) {
			MessagesMod.Instance.OnCategoriesInitialize += func;
		}
	}
}

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
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

		/// <summary></summary>
		public static Message EventsCategoryMsg => ModContent.GetInstance<MessageManager>().EventsCategoryMsg;



		////////////////

		/// <summary>
		/// Indicates if the current, local player has their read messages loaded.
		/// </summary>
		/// <returns></returns>
		public static bool AreMessagesLoadedForCurrentPlayer() {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModLibsException( "Server has no player." );
			}

			if( Main.gameMenu ) {
				return false;
			}

			//

			var myplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );
			return myplayer != null;
		}
	}
}

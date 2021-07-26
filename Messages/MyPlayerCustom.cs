using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using Messages.Logic;


namespace Messages {
	partial class MessagesCustomPlayer : CustomPlayerData {
		private Dictionary<string, HashSet<string>> ReadMessagesByIdsPerWorld = new Dictionary<string, HashSet<string>>();



		////////////////

		protected override void OnEnter( bool isCurrentPlayer, object data ) {
			if( !isCurrentPlayer ) {
				return;
			}

			if( data != null ) {
				this.ReadMessagesByIdsPerWorld = ((JObject)data)
					.ToObject<Dictionary<string, string[]>>()
					.ToDictionary( kv=>kv.Key, kv=>new HashSet<string>( kv.Value ) );
			}

			var mngr = ModContent.GetInstance<MessageManager>();
			mngr.InitializeCategories();

			this.SetReadMessage( mngr.ModInfoCategoryMsg.ID );
			this.SetReadMessage( mngr.HintsTipsCategoryMsg.ID );
			this.SetReadMessage( mngr.GameInfoCategoryMsg.ID );
			this.SetReadMessage( mngr.StoryLoreCategoryMsg.ID );

			MessagesMod.Instance.RunMessageCategoriesInitializeEvent();

			MessagesAPI.AddMessage(
				title: "Remember to set your key bindings!",
				description: "You can view these messages quickly by assigning key bindings in the menu for any "
					+"mods you have that add them (such as this Messages mod).",
				result: out _,
				parent: MessagesAPI.ModInfoCategoryMsg,
				alertPlayer: true
			);
		}


		protected override object OnExit() {
			var data = new Dictionary<string, HashSet<string>>( this.ReadMessagesByIdsPerWorld );

			if( Main.netMode != NetmodeID.Server ) {
				MessagesAPI.ClearMessages( false );
			}

			return data;
		}
	}
}

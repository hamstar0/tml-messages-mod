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

/*LogLibraries.Log( "OnEnter "+isCurrentPlayer+" "+this.PlayerWho
	+Main.gameMenu
	+" "+LoadLibraries.IsWorldLoaded()
	+" "+LoadLibraries.IsWorldBeingPlayed()
	+" "+LoadLibraries.IsWorldSafelyBeingPlayed()
	+" "+LoadLibraries.IsCurrentPlayerInGame()
	+" "+this.ReadMessagesByIdsPerWorld.Select(kv=>kv.Key+": \""+kv.Value.ToStringJoined("\", ")).ToStringJoined("\n")
);*/
			var mngr = ModContent.GetInstance<MessageManager>();
			mngr.ClearAllMessages();

			mngr.InitializeCategories();

			this.SetReadMessage( mngr.ModInfoCategoryMsg.ID );
			this.SetReadMessage( mngr.HintsTipsCategoryMsg.ID );
			this.SetReadMessage( mngr.GameInfoCategoryMsg.ID );
			this.SetReadMessage( mngr.StoryLoreCategoryMsg.ID );
			this.SetReadMessage( mngr.EventsCategoryMsg.ID );
			
			MessagesMod.Instance.RunMessageCategoriesInitializeEvent();
			
			string id = "Messages_Intro";

			MessagesAPI.AddMessage(
				title: "Remember to set your key bindings!",
				description: "You can assign key bindings in the Controls menu for any mods you have that add them "
					+"(such as this Messages mod).",
				modOfOrigin: MessagesMod.Instance,
				alertPlayer: MessagesAPI.IsUnread(id),
				isImportant: false,
				parentMessage: MessagesAPI.ModInfoCategoryMsg,
				id: id
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

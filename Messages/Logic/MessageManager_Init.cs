using System;
using Terraria;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;
using Messages.Definitions;


namespace Messages.Logic {
	partial class MessageManager : ILoadable {
		internal void InitializeCategories() {
			Message msg;
			
			msg = MessagesAPI.AddMessage(
				title: "Mod Info",
				description: "",
				result: out _,
				weight: Int32.MinValue,
				alertPlayer: false
			);
			if( msg != null ) {
				this.ModInfoCategoryMsg = msg;
			}

			msg = MessagesAPI.AddMessage(
				title: "Hints & Tips",
				description: "",
				result: out _,
				weight: Int32.MinValue + 1,
				alertPlayer: false
			);
			if( msg != null ) {
				this.HintsTipsCategoryMsg = msg;
			}
			
			msg = MessagesAPI.AddMessage(
				title: "Game Info",
				description: "",
				result: out _,
				weight: Int32.MinValue + 2,
				alertPlayer: false
			);
			if( msg != null ) {
				this.GameInfoCategoryMsg = msg;
			}

			msg = MessagesAPI.AddMessage(
				title: "Story & Lore",
				description: "",
				result: out _,
				weight: Int32.MinValue + 3,
				alertPlayer: false
			);
			if( msg != null ) {
				this.StoryLoreCategoryMsg = msg;
			}
		}
	}
}

using System;
using Terraria;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;
using Messages.Definitions;


namespace Messages.Logic {
	partial class MessageManager : ILoadable {
		internal void InitializeCategories() {
			Message msg;
			
			msg = this.AddMessage(
				title: "Mod Info",
				description: "",
				parent: null,
				modOfOrigin: MessagesMod.Instance,
				isImportant: false,
				result: out _,
				weight: Int32.MinValue
			);
			if( msg != null ) {
				this.ModInfoCategoryMsg = msg;
			}

			msg = this.AddMessage(
				title: "Hints & Tips",
				description: "",
				parent: null,
				modOfOrigin: MessagesMod.Instance,
				isImportant: false,
				result: out _,
				weight: Int32.MinValue + 1
			);
			if( msg != null ) {
				this.HintsTipsCategoryMsg = msg;
			}
			
			msg = this.AddMessage(
				title: "Game Info",
				description: "",
				modOfOrigin: MessagesMod.Instance,
				isImportant: false,
				parent: null,
				result: out _,
				weight: Int32.MinValue + 2
			);
			if( msg != null ) {
				this.GameInfoCategoryMsg = msg;
			}

			msg = this.AddMessage(
				title: "Story & Lore",
				description: "",
				modOfOrigin: MessagesMod.Instance,
				isImportant: false,
				parent: null,
				result: out _,
				weight: Int32.MinValue + 3
			);
			if( msg != null ) {
				this.StoryLoreCategoryMsg = msg;
			}

			msg = this.AddMessage(
				title: "Events & Interactions",
				description: "",
				modOfOrigin: MessagesMod.Instance,
				isImportant: false,
				parent: null,
				result: out _,
				weight: Int32.MinValue + 4
			);
			if( msg != null ) {
				this.EventsCategoryMsg = msg;
			}
		}
	}
}

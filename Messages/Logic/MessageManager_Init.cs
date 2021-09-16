using System;
using Microsoft.Xna.Framework;
using Terraria;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;
using Messages.Definitions;
using Messages.UI;


namespace Messages.Logic {
	partial class MessageManager : ILoadable {
		internal void InitializeCategories() {
			(Message msg, UIMessage msgElem) = this.AddMessage(
				title: "Mod Info",
				description: "",
				color: Color.Lerp( Color.Aquamarine, Color.Black, 0.5f ),
				parent: null,
				modOfOrigin: MessagesMod.Instance,
				isImportant: false,
				result: out _,
				weight: Int32.MinValue
			);
			if( msg != null ) {
				this.ModInfoCategoryMsg = msg;
			}

			(msg, msgElem) = this.AddMessage(
				title: "Hints & Tips",
				description: "",
				color: Color.Lerp( Color.Chartreuse, Color.Black, 0.5f ),
				parent: null,
				modOfOrigin: MessagesMod.Instance,
				isImportant: false,
				result: out _,
				weight: Int32.MinValue + 1
			);
			if( msg != null ) {
				this.HintsTipsCategoryMsg = msg;
			}

			(msg, msgElem) = this.AddMessage(
				title: "Game Info",
				description: "",
				color: Color.Lerp( Color.DarkOrange, Color.Black, 0.5f ),
				modOfOrigin: MessagesMod.Instance,
				isImportant: false,
				parent: null,
				result: out _,
				weight: Int32.MinValue + 2
			);
			if( msg != null ) {
				this.GameInfoCategoryMsg = msg;
			}

			(msg, msgElem) = this.AddMessage(
				title: "Story & Lore",
				description: "",
				color: Color.Lerp( Color.Fuchsia, Color.Black, 0.5f ),
				modOfOrigin: MessagesMod.Instance,
				isImportant: false,
				parent: null,
				result: out _,
				weight: Int32.MinValue + 3
			);
			if( msg != null ) {
				this.StoryLoreCategoryMsg = msg;
			}

			(msg, msgElem) = this.AddMessage(
				title: "Events & Interactions",
				description: "",
				color: Color.Lerp( Color.LightSalmon, Color.Black, 0.5f ),
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

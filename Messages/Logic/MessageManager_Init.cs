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
			var uiBlue = new Color( 25, 40, 88 );

			(Message msg, UIMessage msgElem) = this.AddMessage(
				title: "Mod Info",
				description: "",
				color: Color.Lerp( Color.Aquamarine, uiBlue, 0.75f ),
				bigTitle: true,
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
				color: Color.Lerp( Color.Chartreuse, uiBlue, 0.75f ),
				bigTitle: true,
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
				color: Color.Lerp( Color.DarkOrange, uiBlue, 0.75f ),
				bigTitle: true,
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
				color: Color.Lerp( Color.Fuchsia, uiBlue, 0.75f ),
				bigTitle: true,
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
				color: Color.Lerp( Color.LightSalmon, uiBlue, 0.75f ),
				bigTitle: true,
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

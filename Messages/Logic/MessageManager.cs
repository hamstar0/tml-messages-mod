using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Theme;
using ModUtilityPanels;
using ModUtilityPanels.Services.UI.UtilityPanels;
using Messages.Definitions;
using Messages.UI;


namespace Messages.Logic {
	partial class MessageManager : ILoadable {
		internal UIMessagesTab MessagesTabUI;


		////////////////

		private object PriorityMessageWidget_Raw;
		
		private UIText PriorityMessageWidgetTextElem;


		////////////////

		public ConcurrentDictionary<string, Message> MessagesByID { get; } = new ConcurrentDictionary<string, Message>();

		public ISet<string> ImportantMessagesByID { get; } = new HashSet<string>();

		////

		public Message ModInfoCategoryMsg { get; private set; }

		public Message HintsTipsCategoryMsg { get; private set; }

		public Message GameInfoCategoryMsg { get; private set; }

		public Message StoryLoreCategoryMsg { get; private set; }

		public Message EventsCategoryMsg { get; private set; }



		////////////////

		void ILoadable.OnModsLoad() {
			if( !Main.dedServ && Main.netMode != NetmodeID.Server ) {
				var utilPanelsMod = (ModUtilityPanelsMod)ModLoader.GetMod( "ModUtilityPanels" );

				utilPanelsMod.OnUtilityPanelsInitialize += () => {
					this.MessagesTabUI = new UIMessagesTab( UITheme.Vanilla );

					// Add tab
					UtilityPanelsTabs.AddTab( MessagesMod.UtilityPanelsTabName, this.MessagesTabUI );

					MessagesMod.Instance.RunMessagesInitializeEvent();
				};
			}
		}

		void ILoadable.OnPostModsLoad() {
			if( !Main.dedServ && Main.netMode != NetmodeID.Server ) {
				this.LoadWidget_If();
			}
		}

		void ILoadable.OnModsUnload() { }


		////////////////
		
		internal void Update() {
			this.UpdateWidget_If();
		}


		////////////////

		public void PostDrawInterface( SpriteBatch spriteBatch ) {
			if( this.IsWidgetMouseHovering ) {
				Main.LocalPlayer.mouseInterface = true;
			}
		}
	}
}

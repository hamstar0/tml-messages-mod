using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;
using ModUtilityPanels.Services.UI.UtilityPanels;


namespace Messages.Logic {
	partial class MessageManager : ILoadable {
		private void LoadWidget_If() {
			if( ModLoader.GetMod("HUDElementsLib") == null ) {
				return;
			}

			//
			
			MessageManager.LoadWidget_WeakRef(
				out this.PriorityMessageWidget_Raw,
				out this.PriorityMessageWidgetTextElem
			);
		}

		private static void LoadWidget_WeakRef(
					out object priorityMessageWidget_raw,
					out UIText priorityMessageTextElem ) {
			var dim = new Vector2( 224f, 52f );
			var pos = new Vector2(
				((float)Main.screenWidth - dim.X) * 0.5f,
				((float)Main.screenHeight - dim.Y) - 64f
			);

			//
			
			var textElem = new UIText( "Important Messages: 0" );
			textElem.HAlign = 0.5f;
			textElem.Top.Set( -12f, 0.5f );

			var widget = new HUDElementsLib.HUDElement(
				name: "Priority Messages",
				position: pos,
				dimensions: dim,
				enabler: () => {
					if( !Main.playerInventory ) {
						return false;
					}

					MessagesAPI.GetUnreadMessageIDs( out ISet<string> msgIds );
					return msgIds.Count > 0;
				}
			);

			widget.Append( textElem );
			widget.OnClick += (_, __) => {
				UtilityPanelsTabs.OpenTab( MessagesMod.UtilityPanelsTabName );
			};

			//

			priorityMessageWidget_raw = widget;
			priorityMessageTextElem = textElem;

			//

			HUDElementsLib.HUDElementsLibAPI.AddWidget( widget );
		}


		////////////////

		private void UpdateWidget_If() {
			if( this.PriorityMessageWidget_Raw == null ) {
				return;
			}

			//

			MessagesAPI.GetUnreadMessageIDs( out ISet<string> msgIds );

			//

			float pulse = (float)Main.mouseTextColor / 255f;

			this.PriorityMessageWidgetTextElem.SetText( "Important Messages: "+msgIds.Count );
			this.PriorityMessageWidgetTextElem.TextColor = new Color(255, 255, (byte)(pulse*pulse*pulse*pulse*255f) );
		}
	}
}

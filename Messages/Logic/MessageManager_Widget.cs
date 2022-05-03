using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using ModLibsCore.Classes.Loadable;
using ModUtilityPanels.Services.UI.UtilityPanels;


namespace Messages.Logic {
	partial class MessageManager : ILoadable {
		private static void LoadWidget_WeakRef(
					out object priorityMessageWidget_raw,
					out UIText priorityMessageTextElem ) {
			var dim = new Vector2( 192f, 32f );
			var pos = new Vector2(
				((float)Main.screenWidth - dim.X) * 0.5f,
				(float)Main.screenHeight - dim.Y //- 32f
			);

			//

			var hudElem = new HUDElementsLib.HUDElement(
				name: "Priority Messages",
				position: pos,
				dimensions: dim,
				enabler: () => {
					MessagesAPI.GetUnreadMessageIDs( out ISet<string> msgIds );
					return msgIds.Count > 0;
				}
			);
			priorityMessageWidget_raw = hudElem;

			//

			priorityMessageTextElem = new UIText( "" );
			hudElem.Append( priorityMessageTextElem );

			//

			hudElem.OnClick += (_, __) => {
				UtilityPanelsTabs.OpenTab( MessagesMod.UtilityPanelsTabName );
			};
		}


		////////////////

		private static void UpdateWidget_If( object priorityMessageWidget_raw, UIText priorityMessageTextElem ) {
			if( priorityMessageWidget_raw == null ) {
				return;
			}

			//

			MessagesAPI.GetUnreadMessageIDs( out ISet<string> msgIds );

			//

			priorityMessageTextElem.SetText( "Important Messages: "+msgIds.Count );
		}
	}
}

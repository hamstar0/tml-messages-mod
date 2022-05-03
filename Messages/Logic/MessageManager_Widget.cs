using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using ModLibsCore.Classes.Loadable;
using ModUtilityPanels.Services.UI.UtilityPanels;


namespace Messages.Logic {
	partial class MessageManager : ILoadable {
		private UIText PriorityMessageElem;



		////////////////

		private void LoadWidget() {
			var dim = new Vector2( 192f, 32f );
			var pos = new Vector2(
				((float)Main.screenWidth - dim.X) * 0.5f,
				(float)Main.screenHeight - dim.Y //- 32f
			);

			//

			var hudElem = new HUDElementsLib.HUDElement(
				name: "Priority Messages",
				position: pos,
				dimensions: dim
			);

			//

			this.PriorityMessageElem = new UIText( "Important Messages" );
			hudElem.Append( this.PriorityMessageElem );

			//

			hudElem.OnClick += (_, __) => {
				UtilityPanelsTabs.OpenTab( MessagesMod.UtilityPanelsTabName );
			};

			//

			HUDElementsLib.HUDElementsLibAPI.AddWidget( hudElem );
		}
	}
}

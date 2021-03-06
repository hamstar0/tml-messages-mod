using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;
using ModUtilityPanels.Services.UI.UtilityPanels;


namespace Messages.Logic {
	partial class MessageManager : ILoadable {
		public static Color GetTextColor( bool isLit ) {
			if( !isLit ) {
				return new Color( 160, 160, 160 );
			} else {
				float pulse = (float)Main.mouseTextColor / 255f;
				return new Color( 255, 255, (byte)(pulse * pulse * pulse * pulse * 255f) );
			}
		}



		////////////////
		
		public bool IsWidgetMouseHovering => (this.PriorityMessageWidget_Raw as UIElement)?
			.IsMouseHovering ?? false;



		////////////////

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
				-dim.X * 0.5f,
				-dim.Y - 64f
			);

			//

			var textElem = new UIText( "Important Messages: 0" );
			textElem.HAlign = 0.5f;
			textElem.Top.Set( -12f, 0.5f );

			var widget = new HUDElementsLib.HUDElement(
				name: "Priority Messages",
				positionOffset: pos,
				positionPercent: new Vector2( 0.5f, 1f ),
				dimensions: dim,
				enabler: () => {
					if( !Main.playerInventory ) {
						return false;
					}

					MessagesAPI.GetUnreadMessageIDs( out ISet<string> msgIds );
					return msgIds.Count > 0;
				}
			);

			widget.OnClick += (_, __) => {
				UtilityPanelsTabs.OpenTab( MessagesMod.UtilityPanelsTabName );

				ModContent.GetInstance<MessageManager>()
					.MessagesTabUI
					.OpenNextUnreadMessage();
			};

			//

			widget.Append( textElem );

			//

			priorityMessageWidget_raw = widget;
			priorityMessageTextElem = textElem;

			//

			HUDElementsLib.HUDElementsLibAPI.AddWidget( widget );
		}


		////////////////

		private void UpdateWidget_Local_If() {
			if( Main.dedServ || Main.netMode == NetmodeID.Server ) {
				return;
			}

			//

			if( this.PriorityMessageWidget_Raw == null ) {
				return;
			}

			//

			MessageManager.UpdateWidget_If_WeakRef(
				this.PriorityMessageWidget_Raw,
				this.PriorityMessageWidgetTextElem 
			);
		}

		private static void UpdateWidget_If_WeakRef( object rawWidget, UIText widgetTextElem ) {
			HUDElementsLib.HUDElement widget = rawWidget as HUDElementsLib.HUDElement;
			if( !widget.IsEnabled() ) {
				return;
			}

			//

			MessagesAPI.GetUnreadMessageIDs( out ISet<string> msgIds );

			//

			widgetTextElem.SetText( "Important Messages: "+msgIds.Count );

			//

			if( widget.IsMouseHovering ) {
				widgetTextElem.TextColor = MessageManager.GetTextColor( true );
			} else {
				widgetTextElem.TextColor = MessageManager.GetTextColor( false );
			}
		}
	}
}

using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Elements;


namespace Messages.UI {
	partial class UIMessage : UIThemedPanel {
		public void SelectMessageFromList( bool recalcContainer ) {
			if( this.ChildMessageElems.Count >= 1 ) {
				if( this.IsTreeExpanded ) {
					this.CollapseTree( true );
				} else {
					this.ExpandTree( true );
				}
			}

			this.BeginDisplayingMessageBody( true );

			if( recalcContainer ) {
				for( UIElement parent = this.Parent; parent != null; parent = parent.Parent ) {
					if( parent is UIList ) {
						parent.Recalculate();
						break;
					}
				}
			}
		}

		////////////////

		public void BeginDisplayingMessageBody( bool viaInterface ) {
			var mycustomplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );
			mycustomplayer.SetReadMessage( this.Message.ID );

			if( viaInterface ) {
				this.OnBodyView?.Invoke();
			}

			var title = new UIThemedTextPanel( this.Theme, false, this.Message.Title, 0.5f, true );
			title.Width.Set( 0f, 1f );
			title.Height.Set( 56f, 0f );

			string subtitleText = "From mod: "+this.Message.ModOfOrigin.DisplayName;
			var subtitle = new UIThemedText( this.Theme, false, subtitleText, true, 0.75f, false );
			subtitle.PaddingTop = 24f;
			subtitle.Width.Set( 0f, 1f );
			subtitle.TextColor = Color.White * 0.35f;
			title.AppendThemed( subtitle );

			this.TabContainer.MessageBodyList.Clear();
			this.TabContainer.MessageBodyList.Add( title );
			this.TabContainer.MessageBodyList.Add( this.DescriptionElem );
		}

		////////////////

		public void ExpandTree( bool viaInterface ) {
			this.InitializeChildMessages();

			if( viaInterface ) {
				this.OnTreeExpand?.Invoke( true );
			}

			this.IsTreeExpanded = true;

			this.Recalculate();
		}


		public void CollapseTree( bool viaInterface ) {
			this.ChildMessagesContainerElem.RemoveAllChildren();

			if( viaInterface ) {
				this.OnTreeExpand?.Invoke( false );
			}

			this.IsTreeExpanded = false;

			this.Recalculate();
		}
	}
}

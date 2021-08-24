using System;
using System.Collections.Generic;
using Terraria;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Classes.PlayerData;


namespace Messages.Definitions {
	public partial class Message {
		public int AddChild( Message message ) {
			int idx = Message.GetMessageIndexInList( this._Children, message );

			this._Children.Insert( idx, message );

			message.Parent = this;

			this.OnChildAdd?.Invoke( idx, message );

			return idx;
		}


		////////////////

		public ISet<string> GetUnreadChildren( bool alsoDescendents ) {
			var myplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );
			var unreadChildren = new HashSet<string>();

			foreach( Message child in this.Children ) {
				if( !myplayer.IsMessageRead(child.ID) ) {
					unreadChildren.Add( child.ID );
				}

				if( alsoDescendents ) {
					unreadChildren.UnionWith( child.GetUnreadChildren(true) );
				}
			}

			return unreadChildren;
		}
	}
}

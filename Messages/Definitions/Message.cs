using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Classes.PlayerData;


namespace Messages.Definitions {
	public partial class Message {
		public static string GenerateMessageID( string title, Mod modOfOrigin ) {
			return modOfOrigin.Name + " - " + title;
		}


		public static int GetMessageIndexInList( IList<Message> msgList, Message msg ) {
			int idx;
			int count = msgList.Count;

			for( idx = 0; idx < count; idx++ ) {
				if( msgList[idx].CompareTo(msg) > 0 ) {
					break;
				}
			}

			return idx;
		}



		////////////////

		protected List<Message> _Children;


		////////////////

		public string ID { get; protected set; }	//Guid.NewGuid().ToString();

		////

		public IReadOnlyList<Message> Children { get; protected set; }

		public Message Parent { get; private set; }

		////

		public string Title { get; protected set; }

		public string Description { get; protected set; }

		////
		
		public Mod ModOfOrigin { get; protected set; }

		////

		public int Weight { get; protected set; }

		////

		public event Action<int, Message> OnChildAdd;



		////////////////

		public Message( string title, string description, Mod modOfOrigin, string id, int weight=0 ) {
			this.ID = id;

			this._Children = new List<Message>();
			this.Children = this._Children.AsReadOnly();

			this.Title = title;
			this.Description = description;
			this.ModOfOrigin = modOfOrigin;
			this.Weight = weight;
		}


		////////////////

		public int AddChild( Message message ) {
			int idx = Message.GetMessageIndexInList( this._Children, message );

			this._Children.Insert( idx, message );

			message.Parent = this;

			this.OnChildAdd?.Invoke( idx, message );

			return idx;
		}


		////////////////

		public bool IsUnread() {
			var myplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );

			return myplayer.IsMessageRead( this.ID );
		}

		////

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


		////////////////
		
		public void SetReadMessage() {
			var myplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );

			myplayer.SetReadMessage( this.ID );
		}


		////////////////

		public int CompareTo( object obj ) {
			var that = obj as Message;
			if( that == null ) {
				return this.Title.CompareTo( obj );
			}

			if( this.Weight > that.Weight ) {
				return 1;
			} else if( this.Weight < that.Weight ) {
				return -1;
			} else {
				return this.Title.CompareTo( that.Title );
			}
		}
	}
}

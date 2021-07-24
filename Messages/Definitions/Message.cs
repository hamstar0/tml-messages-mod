using System;
using System.Collections.Generic;
using Terraria;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Classes.PlayerData;


namespace Messages.Definitions {
	public partial class Message {
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

		public int Weight { get; protected set; }

		////

		public event Action<int, Message> OnChildAdd;



		////////////////

		public Message( string title, string description, string id=null, int weight=0 ) {
			this.ID = id == null
				? title
				: id;

			this._Children = new List<Message>();
			this.Children = this._Children.AsReadOnly();

			this.Title = title;
			this.Description = description;
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

		public ISet<string> GetUnreadChildren() {
			var myplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );
			var unreadChildren = new HashSet<string>();

			foreach( Message child in this.Children ) {
				if( !myplayer.IsMessageRead(child.ID) ) {
					unreadChildren.Add( child.ID );

					unreadChildren.UnionWith( child.GetUnreadChildren() );
				}
			}

			return unreadChildren;
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

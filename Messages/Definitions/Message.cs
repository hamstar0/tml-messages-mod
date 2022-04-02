using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
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

		public Color? Color { get; protected set; }

		////

		public bool BigTitle { get; protected set; }

		////
		
		public Mod ModOfOrigin { get; protected set; }

		////

		public int Weight { get; protected set; }


		////

		public event Action<int, Message> OnChildAdd;



		////////////////

		[Obsolete]
		public Message( string title, string description, Color? color, Mod modOfOrigin, string id, int weight=0 )
			: this( title, description, color, false, modOfOrigin, id, weight ) { }

		public Message(
					string title,
					string description,
					Color? color,
					bool bigTitle,
					Mod modOfOrigin,
					string id,
					int weight=0 ) {
			this.ID = id;

			this._Children = new List<Message>();
			this.Children = this._Children.AsReadOnly();

			this.Title = title;
			this.Description = description;
			this.Color = color;
			this.BigTitle = bigTitle;
			this.ModOfOrigin = modOfOrigin;
			this.Weight = weight;
		}


		////////////////

		public bool IsUnread() {
			var myplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );

			return !myplayer.IsMessageRead( this.ID );
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

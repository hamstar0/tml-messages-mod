﻿using System;
using System.Collections.Generic;
using Terraria;
using ModLibsCore.Libraries.Debug;


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

		////
		
		public string Title { get; protected set; }

		public string Description { get; protected set; }

		////

		public int Weight { get; protected set; }

		////

		public event Action<int, Message> OnChildAdd;



		////////////////

		public Message( string title, string description, string id=null, int weight=0, List<Message> children=null ) {
			this.ID = id == null
				? title
				: id;

			this._Children = children != null
				? children
				: new List<Message>();
			this.Children = this._Children.AsReadOnly();

			this.Title = title;
			this.Description = description;
			this.Weight = weight;
		}


		////////////////

		public int AddChild( Message message ) {
			int idx = Message.GetMessageIndexInList( this._Children, message );

			this._Children.Insert( idx, message );

			this.OnChildAdd?.Invoke( idx, message );

			return idx;
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

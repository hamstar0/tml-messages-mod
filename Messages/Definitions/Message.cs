using System;
using Terraria;
using ModLibsCore.Libraries.Debug;


namespace Messages.Definitions {
	public partial class Message {
		public string ID { get; protected set; }	//Guid.NewGuid().ToString();

		////

		public Message Parent { get; protected set; } = null;

		////

		public string Title { get; protected set; }

		public string Description { get; protected set; }

		////

		public int Weight { get; protected set; }



		////////////////

		public Message( string title, string description, string id=null, int weight=0, Message parent=null ) {
			this.Title = title;
			this.Description = description;
			this.Weight = weight;
			this.Parent = parent;

			this.ID = id == null
				? title
				: id;
		}
	}
}

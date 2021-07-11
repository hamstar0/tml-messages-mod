using System;
using Terraria;
using ModLibsCore.Libraries.Debug;


namespace Messages.Definitions {
	public partial class Message {
		public bool IsRead = false;


		////////////////

		public string Title { get; protected set; }

		public string Description { get; protected set; }

		public int Weight { get; protected set; }



		////////////////

		public Message( string title, string description, int weight=0 ) {
			this.Title = title;
			this.Description = description;
			this.Weight = weight;
		}
	}
}

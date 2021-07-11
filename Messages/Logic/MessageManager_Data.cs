using System;
using System.Linq;
using Terraria;
using ModLibsCore.Classes.Loadable;
using Messages.Definitions;


namespace Messages.Logic {
	partial class MessageManager : ILoadable {
		public Message[] GetMessages() {
			return this.CurrentMessages.Values.ToArray();
			/*return this.CurrentMessageOrder
				.Select( title => this.CurrentMessages[title] )
				.ToArray();*/
		}


		////

		private bool AddMessageData( Message message, out string result ) {
			if( this.CurrentMessages.ContainsKey(message.Title) ) {
				result = "Message named "+message.Title+" already defined.";
				return false;
			}

			this.CurrentMessages[ message.Title ] = message;

			result = "Success.";
			return true;
		}

		private void RemoveMessageData( string title ) {
			this.CurrentMessages.TryRemove( title, out Message _ );
		}

		private void ClearMessagesData() {
			this.CurrentMessages.Clear();
		}
	}
}

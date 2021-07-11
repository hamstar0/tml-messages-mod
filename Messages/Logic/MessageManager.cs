using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using ModLibsCore.Classes.Loadable;
using Messages.Definitions;


namespace Messages.Logic {
	partial class MessageManager : ILoadable {
		private int _UpdateTimer = 0;


		////////////////
		
		public ConcurrentDictionary<string, Message> CurrentMessages { get; } = new ConcurrentDictionary<string, Message>();



		////////////////

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnPostModsLoad() { }

		void ILoadable.OnModsUnload() { }


		////////////////

		internal void Update_Internal() {
			if( this._UpdateTimer-- <= 0 ) {
				this._UpdateTimer = 60;
			} else {
				return;
			}

			//foreach( Message obj in this.CurrentMessages.Values ) {
			//	if( obj.Update_Internal() ) {
			//		this.NotifySubscribers( obj, false );
			//	}
			//}
		}
	}
}

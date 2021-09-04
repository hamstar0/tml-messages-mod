using System;
using Terraria;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;


namespace Messages {
	/// <summary>
	/// Supplies API functions.
	/// </summary>
	public partial class MessagesAPI {
		/// <summary></summary>
		/// <param name="messageId"></param>
		/// <returns></returns>
		public static bool IsUnread( string messageId ) {
			var myplayer = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );

			return !myplayer.IsMessageRead( messageId );
		}

		/// <summary>
		/// Sets a message as read or unread.
		/// </summary>
		/// <param name="messageId"></param>
		/// <param name="isRead"></param>
		/// <returns></returns>
		public static bool SetMessageReadState( string messageId, bool isRead ) {
			var mycustplr = CustomPlayerData.GetPlayerData<MessagesCustomPlayer>( Main.myPlayer );

			return isRead
				? mycustplr.SetReadMessage( messageId )
				: mycustplr.UnsetReadMessage( messageId );
		}
	}
}

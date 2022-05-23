using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using Messages.Definitions;


namespace Messages {
	/// <summary>
	/// Supplies API functions.
	/// </summary>
	public partial class MessagesAPI {
		[Obsolete( "use AddMessage", true )]
		public static (Message msg, string result) AddMessageNoColor(
					string title,
					string description,
					Mod modOfOrigin,
					bool alertPlayer,
					bool isImportant,
					Message parentMessage,
					string id = null,
					int weight = 0 ) {
			return MessagesAPI.AddMessage(
				title: title,
				description: description,
				modOfOrigin: modOfOrigin,
				alertPlayer: alertPlayer,
				isImportant: isImportant,
				parentMessage: parentMessage,
				id: id,
				weight: weight
			);
		}

		[Obsolete("use AddColoredMessage", true)]
		public static (Message msg, string result) AddMessage(
					string title,
					string description,
					Color? color,
					Mod modOfOrigin,
					bool alertPlayer,
					bool isImportant,
					Message parentMessage,
					string id = null,
					int weight = 0 ) {
			return MessagesAPI.AddColoredMessage(
				title: title,
				description: description,
				color: color.HasValue ? color.Value : Color.White,
				modOfOrigin: modOfOrigin,
				alertPlayer: alertPlayer,
				isImportant: isImportant,
				parentMessage: parentMessage,
				id: id,
				weight: weight
			);
		}
	}
}

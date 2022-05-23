using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;
using ModUtilityPanels.Services.UI.UtilityPanels;
using Messages.Definitions;
using Messages.Logic;
using Messages.UI;


namespace Messages {
	/// <summary>
	/// Supplies API functions.
	/// </summary>
	public partial class MessagesAPI {
		/// <summary>Adds a message to the list. Note: Messages that area already read will not be added.</summary>
		/// <param name="title"></param>
		/// <param name="description"></param>
		/// <param name="modOfOrigin"></param>
		/// <param name="alertPlayer"></param>
		/// <param name="isImportant">Creates a different type of alert for the given message.</param>
		/// <param name="parentMessage">"Folder" message (`Message`) for the current message to belong to.</param>
		/// <returns>A non-null `Message` if message was registered successfully (i.e. no duplicates found).</returns>
		/// <param name="id">Allows for duplicate messages. Defaults to using `title` if null.</param>
		/// <param name="weight">Sort order priority of message in descending order.</param>
		public static (Message msg, string result) AddMessage(
					string title,
					string description,
					Mod modOfOrigin,
					bool alertPlayer,
					bool isImportant,
					Message parentMessage,
					string id = null,
					int weight = 0 ) {
			return MessagesAPI.AddMessageWithColorData(
				title: title,
				description: description,
				color: (Color?)null,
				modOfOrigin: modOfOrigin,
				alertPlayer: alertPlayer,
				isImportant: isImportant,
				parentMessage: parentMessage,
				id: id,
				weight: weight
			);
		}

		/// <summary>Adds a message to the list. Note: Messages that area already read will not be added.</summary>
		/// <param name="title"></param>
		/// <param name="description"></param>
		/// <param name="color"></param>
		/// <param name="modOfOrigin"></param>
		/// <param name="alertPlayer"></param>
		/// <param name="isImportant">Creates a different type of alert for the given message.</param>
		/// <param name="parentMessage">"Folder" message (`Message`) for the current message to belong to.</param>
		/// <returns>A non-null `Message` if message was registered successfully (i.e. no duplicates found).</returns>
		/// <param name="id">Allows for duplicate messages. Defaults to using `title` if null.</param>
		/// <param name="weight">Sort order priority of message in descending order.</param>
		public static (Message msg, string result) AddMessageWithColor(
					string title,
					string description,
					Color color,
					Mod modOfOrigin,
					bool alertPlayer,
					bool isImportant,
					Message parentMessage,
					string id = null,
					int weight = 0 ) {
			return MessagesAPI.AddMessageWithColorData(
				title: title,
				description: description,
				color: color,
				modOfOrigin: modOfOrigin,
				alertPlayer: alertPlayer,
				isImportant: isImportant,
				parentMessage: parentMessage,
				id: id,
				weight: weight
			);
		}

		private static (Message msg, string result) AddMessageWithColorData(
					string title,
					string description,
					Color? color,
					Mod modOfOrigin,
					bool alertPlayer,
					bool isImportant,
					Message parentMessage,
					string id = null,
					int weight = 0 ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModLibsException( "Server messages not allowed." );
			}
			if( !MessagesMod.Instance.IsMessageTabCategoriesInitialized ) {
				throw new ModLibsException( "Message display not finished initializing." );
			}
			if( parentMessage != null && !(parentMessage is Message) ) {
				throw new ModLibsException( "Invalid parentMessage." );
			}

			//

			var mngr = ModContent.GetInstance<MessageManager>();
			
			(Message msg, UIMessage msgElem) = mngr.AddMessage(
				title: title,
				description: description,
				color: color,
				bigTitle: false,
				modOfOrigin: modOfOrigin,
				isImportant: isImportant,
				parent: parentMessage as Message,
				result: out string result,
				id: id,
				weight: weight
			);

			//
			
			if( result == "Success." ) {
				if( alertPlayer ) {
					if( isImportant ) {
						Main.PlaySound( SoundID.Zombie, -1, -1, 70, 0.5f, 0f );
						Timers.SetTimer( 10, true, () => {
							Main.PlaySound( type: SoundID.Zombie, x: -1, y: -1, Style: 70 );    //volumeScale: 0.5f
							return false;
						} );

						Main.NewText( "Incoming message \""+title+"\"", new Color(255, 255, 128) );
					}

					//

					UtilityPanelsTabs.AddTabAlert( MessagesMod.UtilityPanelsTabName, isImportant );

					MessagesMod.Instance.ShowAlert( isImportant );
				}
			} else {
				//LogLibraries.AlertOnce( "Message failed ('"+title+"'): "+result );
			}

			return (msg, result);
		}
	}
}

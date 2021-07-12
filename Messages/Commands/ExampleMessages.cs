using System;
using Terraria;
using Terraria.ModLoader;
using Messages.Definitions;


namespace Messages.Commands {
	public class ExampleMessagesCommand : ModCommand {
		/// @private
		public override CommandType Type => CommandType.Chat;
		/// @private
		public override string Command => "m-example";
		/// @private
		public override string Usage => "/" + this.Command;
		/// @private
		public override string Description => "Creates a few example messages.";


		////////////////

		/// @private
		public override void Action( CommandCaller caller, string input, string[] args ) {
			string _;

			MessagesAPI.AddMessage(
				title: "Kill A Blue Slime",
				description: "What even is a slime?",
				alertPlayer: false
			);

			MessagesAPI.AddMessage(
				title: "Collect 50 Rings",
				description: "Wrong game.\n \n...might be a fun mod, though?",
				alertPlayer: true
			);

			MessagesAPI.AddMessage(
				title: "Order Pizza",
				description: "Can't be done.",
				alertPlayer: false
			);

			MessagesAPI.AddMessage(
				title: "Collect A Blueberry",
				description: "Don't ask.",
				alertPlayer: true
			);

			MessagesAPI.AddMessage(
				title: "Craft A Molotov",
				description: "Viva la revolution!",
				alertPlayer: false
			);
		}
	}
}

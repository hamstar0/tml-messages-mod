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

			/*Message msg1 = MessagesAPI.AddMessage(
				title: "Kill A Blue Slime",
				description: "What even is a slime?",
				alertPlayer: false,
				result: out _
			);

			MessagesAPI.AddMessage(
				title: "Kill A Red Slime",
				description: "Opposing colors!",
				alertPlayer: false,
				parent: msg1,
				result: out _
			);

			MessagesAPI.AddMessage(
				title: "Collect 50 Rings",
				description: "Wrong game.\n \n...might be a fun mod, though?",
				alertPlayer: true,
				result: out _
			);

			Message msg2 = MessagesAPI.AddMessage(
				title: "Order Pizza",
				description: "Can't be done.",
				alertPlayer: false,
				result: out _
			);

			Message msg3 = MessagesAPI.AddMessage(
				title: "Collect A Blueberry",
				description: "Don't ask.",
				alertPlayer: true,
				parent: msg2,
				result: out _
			);

			MessagesAPI.AddMessage(
				title: "Craft A Molotov",
				description: "Viva la revolution!",
				alertPlayer: false,
				parent: msg3,
				result: out _
			);*/

			this.CreateSpam( " 1" );
			this.CreateSpam( " 2" );
			this.CreateSpam( " 3" );
		}

		private void CreateSpam( string affix ) {
			Message msg1 = MessagesAPI.AddMessage(
				title: "Order Pizza"+affix,
				description: "Can't be done.",
				alertPlayer: false,
				result: out string result
			);
			if( msg1 == null ) {
				Main.NewText( "Error loading message 1: "+result );
			}

			Message msg2 = MessagesAPI.AddMessage(
				title: "Collect A Blueberry"+affix,
				description: "",
				alertPlayer: true,
				parent: msg1,
				result: out _
			);
			if( msg2 == null ) {
				Main.NewText( "Error loading message 2: "+result );
			}

			Message msg3 = MessagesAPI.AddMessage(
				title: "Craft A Molotov"+affix,
				description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
				alertPlayer: false,
				parent: msg2,
				result: out _
			);
			if( msg3 == null ) {
				Main.NewText( "Error loading message 3: "+result );
			}
		}
	}
}

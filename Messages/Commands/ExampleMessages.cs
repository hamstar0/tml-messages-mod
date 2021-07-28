using System;
using Terraria;
using Terraria.ModLoader;
using Messages.Definitions;
using Messages.Logic;


namespace Messages.Commands {
	public class ExampleMessagesCommand : ModCommand {
		/// @private
		public override CommandType Type => CommandType.Chat;
		/// @private
		public override string Command => "msg-example";
		/// @private
		public override string Usage => "/" + this.Command;
		/// @private
		public override string Description => "Creates a few example messages.";


		////////////////

		/// @private
		public override void Action( CommandCaller caller, string input, string[] args ) {
			var mngr = ModContent.GetInstance<MessageManager>();
			(Message msg, string result) msgData;
			string _;

			MessagesAPI.AddMessage(
				title: "This is a sample message",
				description: "This messages exists on its own.",
				modOfOrigin: this.mod,
				parentMessage: mngr.ModInfoCategoryMsg,
				alertPlayer: true
			);

			MessagesAPI.AddMessage(
				title: "This is another sample message",
				description: "Yay!",
				modOfOrigin: this.mod,
				parentMessage: mngr.ModInfoCategoryMsg,
				alertPlayer: true
			);

			MessagesAPI.AddMessage(
				title: "This is a really long message",
				description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
					+"\n \n"+"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
					+"\n \n"+"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
					+"\n \n"+"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
					+"\n \n"+"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
					+"\n \n"+"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
					+"\n \n"+"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
					+"\n \n"+"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
					+"\n \n"+"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
					+"\n \n"+"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
				modOfOrigin: this.mod,
				alertPlayer: true,
				parentMessage: mngr.ModInfoCategoryMsg
			);

			msgData = MessagesAPI.AddMessage(
				title: "This is a message as a folder",
				description: "",
				modOfOrigin: this.mod,
				alertPlayer: true,
				parentMessage: mngr.ModInfoCategoryMsg
			);

			MessagesAPI.AddMessage(
				title: "Sub-message 1",
				description: "I'm a message.",
				modOfOrigin: this.mod,
				alertPlayer: true,
				parentMessage: msgData.msg
			);

			 MessagesAPI.AddMessage(
				title: "Sub-message 2",
				description: "I'm a message.",
				modOfOrigin: this.mod,
				alertPlayer: true,
				parentMessage: msgData.msg
			);

			MessagesAPI.AddMessage(
				title: "Sub-message 3",
				description: "I'm a message.",
				modOfOrigin: this.mod,
				alertPlayer: true,
				parentMessage: msgData.msg
			);

			MessagesAPI.AddMessage(
				title: "Sub-message 4",
				description: "I'm a message.",
				modOfOrigin: this.mod,
				alertPlayer: true,
				parentMessage: msgData.msg
			);

			MessagesAPI.AddMessage(
				title: "Sub-message 5",
				description: "I'm a message.",
				modOfOrigin: this.mod,
				alertPlayer: true,
				parentMessage: msgData.msg
			);

			msgData = MessagesAPI.AddMessage(
				title: "Sub-message 6",
				description: "I'm a message.",
				modOfOrigin: this.mod,
				alertPlayer: true,
				parentMessage: msgData.msg
			);

			MessagesAPI.AddMessage(
				title: "Sub-sub-message 6",
				description: "I'm a sub message.",
				modOfOrigin: this.mod,
				alertPlayer: true,
				parentMessage: msgData.msg
			);
		}
	}
}

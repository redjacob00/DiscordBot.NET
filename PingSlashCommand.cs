using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
	public class PingSlashCommand : SlashCommand
	{
		public PingSlashCommand()
		{
			_name = "ping";
			_description = "Responds back with 'pong!'";
		}

		public async override void Execute(SocketSlashCommand command)
		{
			await command.RespondAsync("pong!");
		}
	}
}

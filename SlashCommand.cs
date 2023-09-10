using Discord;
using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
	public abstract class SlashCommand
	{
		public string Name { get { return _name; }  }
		protected string _name;
		protected string _description;

		public SlashCommandProperties Build()
		{
			SlashCommandBuilder builder = new();
			builder.WithName(_name);
			builder.WithDescription(_description);
			return builder.Build();
		}
		public abstract void Execute(SocketSlashCommand command);
	}
}

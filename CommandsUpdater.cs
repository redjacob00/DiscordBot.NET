using Discord;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace DiscordBot
{
	public static class CommandsUpdater
	{
		public static async Task UpdateGuildCommands(DiscordSocketClient client, ulong guildId, SlashCommand[] commands)
		{
			SocketGuild guild = client.GetGuild(guildId);

			try
			{
				foreach (SlashCommand command in commands)
				{
					await guild.CreateApplicationCommandAsync(command.Build());
				}
			}
			catch (HttpException e)
			{
				string json = JsonConvert.SerializeObject(e.Errors, Formatting.Indented);
				Console.WriteLine(json);
			}
		}

		public static async Task UpdateGlobalCommands(DiscordSocketClient client, SlashCommand[] commands)
		{
			try
			{
				foreach(SlashCommand command in commands)
				{
					await client.CreateGlobalApplicationCommandAsync(command.Build());
				}
			}
			catch (HttpException e)
			{
				string json = JsonConvert.SerializeObject(e.Errors, Formatting.Indented);
				Console.WriteLine(json);
			}
		}
	}
}
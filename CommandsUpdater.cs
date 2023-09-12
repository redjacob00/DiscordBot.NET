using Discord;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;
using System.Data;

namespace DiscordBot
{
	public static class CommandsUpdater
	{
		public static async Task UpdateAllGuildCommands(DiscordSocketClient client)
		{
			Dictionary<ulong, SlashCommand[]> guildsCommands = new()
			{
				// ulong: the id of the guild, SlashCommand[]: the commands you wish to be available for only that guild
			};

			try
			{
				foreach (var kvp in guildsCommands)
				{
					await UpdateGuildCommands(client, kvp.Key, kvp.Value);
				}
			} catch (HttpException) { }
		}

		public static async Task UpdateGuildCommands(DiscordSocketClient client, ulong guildId, SlashCommand[] guildCommands)
		{
			SocketGuild guild = client.GetGuild(guildId);

			try
			{
				foreach (SlashCommand command in guildCommands)
				{
					await guild.CreateApplicationCommandAsync(command.Build());
					Console.WriteLine($"guild '{guild.Name}' has had its command '{command.Name}' updated");
				}
			}
			catch (HttpException e)
			{
				string json = JsonConvert.SerializeObject(e.Errors, Formatting.Indented);
				Console.WriteLine(json);
			}
		}

		public static async Task UpdateGlobalCommands(DiscordSocketClient client)
		{
			SlashCommand[] globalCommands = new SlashCommand[] {

			};

			try
			{
				foreach(SlashCommand command in globalCommands)
				{
					await client.CreateGlobalApplicationCommandAsync(command.Build());
					Console.WriteLine($"command '{command.Name}' has been updated globally");
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
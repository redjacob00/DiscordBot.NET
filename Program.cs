using Discord;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;
using DiscordBot;
/*
 * TODO
 * 1. make Command abstract class
 * 2. make CommandsUpdater.UpdateGuildCommands update specific
 */
class Program
{
	private DiscordSocketClient _client = new();
	private readonly SlashCommand[] slashCommands = new SlashCommand[]
	{
		new PingSlashCommand(),
	};
	private readonly SlashCommand[] globalSlashCommands = new SlashCommand[]
	{

	};
	private readonly Dictionary<ulong, SlashCommand[]> guildsCommands = new()
	{
		// ulong: the id of the guild, SlashCommand[]: the commands you wish to be available for only that guild
	};

	public static Task Main(string[] args) => new Program().MainAsync(args);
	public async Task MainAsync(string[] args)
	{
		_client = new DiscordSocketClient();
		_client.Log += Log;
		string token = "your token";
		await _client.LoginAsync(TokenType.Bot, token);
		await _client.StartAsync();
		_client.SlashCommandExecuted += SlashCommandHandler;
		if (args.Length > 0)
		{
			Task.Delay(5000).Wait(); // Wait until socket is fully loaded
			if (args[0] == "updguild")
			{
				foreach (var kvp in guildsCommands) { 
					await CommandsUpdater.UpdateGuildCommands(_client, kvp.Key, kvp.Value);
				}
				Console.WriteLine("Guild Commands Updated!");
			} else if (args[0] == "updglobal") {
				foreach (var kvp in guildsCommands)
				{
					await CommandsUpdater.UpdateGuildCommands(_client, kvp.Key, kvp.Value);
				}
				Console.WriteLine("Global Commands Updated!");
			}
		}
		await Task.Delay(-1);
	}

	private Task Log(LogMessage message)
	{
		Console.WriteLine(message.ToString());
		return Task.CompletedTask;
	}

	private Task SlashCommandHandler(SocketSlashCommand command)
	{
		foreach (SlashCommand slashCommand in slashCommands)
		{
			if (slashCommand.Name == command.CommandName)
			{
				slashCommand.Execute(command);
				break;
			}
		}

		return Task.CompletedTask;
	}
}
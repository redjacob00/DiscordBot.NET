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
	

	public static Task Main(string[] args) => new Program().MainAsync(args);
	public async Task MainAsync(string[] args)
	{
		_client = new DiscordSocketClient();
		_client.Log += Log;
		string token = "Mzc4NjM3NzgwMTA2MDg0MzYz.GWdgaA.fRnHyAtcmuo0KppydsvU-VM_BqywXYjRYoozj4";
		await _client.LoginAsync(TokenType.Bot, token);
		await _client.StartAsync();
		_client.SlashCommandExecuted += SlashCommandHandler;
		if (args.Length > 0)
		{
			Task.Delay(5000).Wait(); // Wait until socket is fully loaded
			if (args[0] == "updguild")
			{
				await CommandsUpdater.UpdateAllGuildCommands(_client);
			} else if (args[0] == "updglobal") {
				await CommandsUpdater.UpdateGlobalCommands(_client);
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
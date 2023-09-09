using Discord;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;

class Program
{
	private DiscordSocketClient _client;

	public static Task Main(string[] args) => new Program().MainAsync();
	public async Task MainAsync()
	{
		_client = new DiscordSocketClient();
		_client.Log += Log;
		string token = "Mzc4NjM3NzgwMTA2MDg0MzYz.GGO2qE.fn01UiKvet-SGyQ1LkU5M_YSyceOvTOMmAvd2c";
		await _client.LoginAsync(TokenType.Bot, token);
		await _client.StartAsync();
		//Task.Delay(2000).Wait();
		//await SetUpSlashCommands();
		_client.SlashCommandExecuted += SlashCommandHandler;
		await Task.Delay(-1);
	}

	private Task Log(LogMessage message)
	{
		Console.WriteLine(message.ToString());
		return Task.CompletedTask;
	}

	private async Task SetUpSlashCommands()
	{
		var guild = _client.GetGuild(857093410371665930);
		SlashCommandBuilder guildCommand = new();
		guildCommand.WithName("ping");
		guildCommand.WithDescription("pong!");

		try
		{
			// updates/adds a new slash command to a guild
			await guild.CreateApplicationCommandAsync(guildCommand.Build());
			// updates/adds a new slash command globally
			//await _client.CreateGlobalApplicationCommandAsync(guildCommand.Build());
		} catch (HttpException e)
		{
			string json = JsonConvert.SerializeObject(e.Errors, Formatting.Indented);
			Console.WriteLine(json);
		}
	}

	private async Task SlashCommandHandler(SocketSlashCommand command)
	{
		if (command.Data.Name == "ping")
		{
			await command.RespondAsync("pong!");
		}
	}
}
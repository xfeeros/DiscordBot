using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using Serilog;

public class Program
{
    private DiscordSocketClient _client;

    public static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

    public Program()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();

        _client = new DiscordSocketClient();
        _client.Log += LogAsync;
        _client.Ready += ReadyAsync;
        _client.MessageReceived += MessageReceivedAsync;
    }

    private Task LogAsync(LogMessage log)
    {
        Log.Debug(log.Message);
        return Task.CompletedTask;
    }

    private Task ReadyAsync()
    {
        Log.Information("Bot is connected!");
        return Task.CompletedTask;
    }

    public async Task MainAsync()
    {
        string token = System.IO.File.ReadAllText("Token.txt");
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();
        await Task.Delay(-1);
    }

    private async Task MessageReceivedAsync(SocketMessage message)
    {
        Log.Information($"Received message: {message.Content} from {message.Author.Username}");

        if (message.Content == "!join")
        {
            if (message.Author is SocketGuildUser user)
            {
                var voiceChannel = user.VoiceChannel;
                if (voiceChannel != null)
                {
                    var audioClient = await voiceChannel.ConnectAsync();
                    await message.Channel.SendMessageAsync("Joined the voice channel!");
                }
                else
                {
                    await message.Channel.SendMessageAsync("You must be in a voice channel for me to join!");
                }
            }
        }
        else if (message.Content == "!roll")
        {
            var random = new Random();
            int result = random.Next(1, 101);
            await message.Channel.SendMessageAsync($"Rolled a number: {result}");
        }
        else if (message.Content == "!help")
        {
            await message.Channel.SendMessageAsync("Available commands: !join - join a voice channel, !roll - generate a random number.");
        }
    }
}

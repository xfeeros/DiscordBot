using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

public class Program
{
    private DiscordSocketClient _client;

    public static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

    public Program()
    {
        _client = new DiscordSocketClient();
        _client.Log += LogAsync;
        _client.Ready += ReadyAsync;
        _client.MessageReceived += MessageReceivedAsync;
    }

    private Task LogAsync(LogMessage log)
    {
        Console.WriteLine(log);
        return Task.CompletedTask;
    }

    private Task ReadyAsync()
    {
        Console.WriteLine("Bot is connected!");
        return Task.CompletedTask;
    }

    public async Task MainAsync()
    {
        //string token = "Token.txt";
        string token = System.IO.File.ReadAllText("Token.txt");
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();
        await Task.Delay(-1);
    }

    private async Task MessageReceivedAsync(SocketMessage message)
    {
        if (message.Content == "!join")
        {
            if (message.Author is SocketGuildUser user)
            {
                var voiceChannel = user.VoiceChannel;
                if (voiceChannel != null)
                {
                    var audioClient = await voiceChannel.ConnectAsync();
                    await message.Channel.SendMessageAsync("Присоединился к голосовому каналу!");
                }
                else
                {
                    await message.Channel.SendMessageAsync("Вы должны находиться в голосовом канале, чтобы я мог присоединиться!");
                }
            }
        }
        else if (message.Content == "!roll")
        {
            var random = new Random();
            int result = random.Next(1, 101);
            await message.Channel.SendMessageAsync($"Выпало число: {result}");
        }
    }
}
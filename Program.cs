using Discord;
using Discord.WebSocket;

public class Program
{
    string token = System.IO.File.ReadAllText("Token.txt");
    private readonly DiscordSocketClient _client;

    public static void Main(string[] args)
    => new Program().MainAsync().GetAwaiter().GetResult();

    public Program()
    {
        _client = new DiscordSocketClient();

        _client.Log += LogAsync;
        _client.MessageReceived += HandleCommandAsync;
    }

    private async Task LogAsync(LogMessage log)
    {
        Console.WriteLine(log);
    }

    public async Task MainAsync()
    {
        string token = System.IO.File.ReadAllText("Token.txt");
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();
        await Task.Delay(-1);
    }

    private async Task HandleCommandAsync(SocketMessage messageParam)
    {
        var message = messageParam as SocketUserMessage;
        if (message == null) return;

        if (message.Content == "!join")
        {
            var user = message.Author as SocketGuildUser;
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
}
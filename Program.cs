using Discord;
using Discord.WebSocket;
using dotenv.net;

public class Program
{
    private readonly DiscordSocketClient _client;

    public static void Main(string[] args)
    => new Program().MainAsync().GetAwaiter().GetResult();

    public Program()
    {
        _client = new DiscordSocketClient();

        _client.Log += LogAsync;
        _client.MessageReceived += HandleCommandAsync;
        _client.UserVoiceStateUpdated += HandleVoiceStatusUpdate;
 
    }

    private async Task LogAsync(LogMessage log)
    {
        Console.WriteLine(log);
        
    }

    public async Task MainAsync()
    {
        DotEnv.Load();

        var config = DotEnv.Read();
        string token = config["TOKEN"];

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();
        await Task.Delay(-1);
    }

    private Task HandleVoiceStatusUpdate(SocketUser user, SocketVoiceState state, SocketVoiceState state2)
    {
        if (state.ToString() == "Unknown")
        {
            Console.WriteLine($"User: {user.Username} joined to {state2}");
        } else
        {
            Console.WriteLine($"User: {user.Username} left from {state}");
        }
        
        return Task.CompletedTask;
    }

    private Task HandleCommandAsync(SocketMessage messageParam)
    {
        var message = messageParam as SocketUserMessage;
        Console.WriteLine(message.Content);
        return Task.CompletedTask;
    }
}
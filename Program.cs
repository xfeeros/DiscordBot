using System;
using System.Threading.Tasks;
using dotenv.net;
using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace SimpleBot
{
    public class Program
    {
        public static async Task Main()
        {
            DotEnv.Load();
            var config = DotEnv.Read();
            string token = config["TOKEN"];
            DiscordClient client = new DiscordClient(new DiscordConfiguration
            {
                Token = token,
                TokenType = TokenType.Bot,

                Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents
            });

            CommandsNextExtension commands = client.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefixes = new[] { "!" }
            });

            // Register commands
            commands.RegisterCommands<SimpleBotCommands>();

            await client.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
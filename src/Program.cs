using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using dotenv.net;
using DSharpPlus;
using DSharpPlus.AsyncEvents;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;

namespace DiscordBot.src
{
    public class Program
    {

        public static async Task Main()
        {
            DotEnv.Load();
            var config = DotEnv.Read();
            string token = config["TOKEN"];
            string guild = config["GUILD"];
            DiscordClient client = new DiscordClient(new DiscordConfiguration
            {
                Token = token,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents
            });

            var slash = client.UseSlashCommands();

            // Command handler
            slash.RegisterCommands<CommandManager>((ulong)Decimal.Parse(guild));

            // client.VoiceStateUpdated += VoiceServer;
            // client.VoiceStateUpdated += VoiceServer;
            



            await client.ConnectAsync();
            await Task.Delay(-1);
        }

        /*
        private static async Task VoiceServer(DiscordClient client, VoiceStateUpdateEventArgs ev)
        {
            Console.WriteLine(ev.Before.ToString());



            await client.ConnectAsync();
            await Task.Delay(-1);
        }
        */
    }
}
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

public class SimpleBotCommands : BaseCommandModule
{
    [Command("test")]
    public async Task Ping(CommandContext ctx)
    {
        await ctx.RespondAsync("Тест хуйни");
    }
}

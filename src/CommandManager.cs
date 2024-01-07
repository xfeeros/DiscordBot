using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

public class CommandManager : ApplicationCommandModule
{
    [SlashCommand("пинг", "базированная команда")]
    public async Task DelayTestCommand(InteractionContext ctx)
    {
        await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

        await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Понг."));
    }
}

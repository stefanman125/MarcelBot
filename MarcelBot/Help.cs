using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace MarcelBot.Core.Commands
{
    public class Help : ModuleBase<SocketCommandContext>
    {
        [Command("Help"), Alias("commands", "command", "man", "manual"), Summary("Help command, lists commands for Marcel")]
        public async Task TaskHelp()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithAuthor(Context.Guild.CurrentUser);
            Embed.WithColor(0, 181, 247);
            Embed.WithTitle("Marcel Jar's Command List");
            Embed.WithDescription("`Hey`: Greet Marcel! (You can add something after the command to initiate a dad joke)\n" +
                "`I'm`: Tell Marcel how you're feeling (remember to add something after the 'I'm')\n" +
                "`Countdown`: Check how many days are left until the end of the semester\n" +
                "`Joke`: Ask Marcel to tell you a joke!" +
                "\n\n-- More to come soon! --");
            Embed.WithFooter("" + DateTime.Now);
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }
    }
}
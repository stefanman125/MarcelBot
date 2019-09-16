using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace MarcelBot
{
    public class Hey : ModuleBase<SocketCommandContext>
    {
        [Command("hey"), Alias("Hey", "hey,", "Hey,", "Hi", "hi", "yo", "im", "i'm", "Im", "I'm"), Summary("Hey command. This makes Marcel tell a dad joke.")]
        public async Task TaskHey([Remainder]string userInput = "")
        {
            if (userInput == "")
            {
                await Context.Channel.SendMessageAsync("Hey, " + Context.User.Mention + "! I'm dad!");
            }
            else
            {
                await Context.Channel.SendMessageAsync("Hey, " + userInput + "! I'm dad!");
            }
        }
    }
}
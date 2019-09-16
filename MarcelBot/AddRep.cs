using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using Discord;
using Discord.Commands;

namespace MarcelBot.Core.Commands
{
    public class AddRep : ModuleBase<SocketCommandContext>
    {
        [Command("AddRep"), Alias("addrep", "addRep", "Addrep"), Summary("Adds more reputation to users current score"), RequireUserPermission(GuildPermission.Administrator)]
        public async Task TaskAddRep()
        {
            /*
            var user = Context.User as SocketGuildUser;
            
            if (user.Roles.Any(r => r.Name == "Robot"))
            {

            }
            
            await Context.Channel.SendMessageAsync(""+Context.User.Username+" | "+Context.User.Discriminator);
            StringBuilder csvContent = new StringBuilder();
            csvContent.AppendLine("stefan#1853,0,0");
            System.IO.File.AppendAllText("repValues.csv", csvContent.ToString());
            */
        }
    }
}
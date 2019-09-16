using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace MarcelBot.Core.Commands
{
    public class ProxyMessage : ModuleBase<SocketCommandContext>
    {
        [Command("ProxyMessage"), Alias("proxy"), Summary("Sends a message in Marcels context rather than the admins."), RequireUserPermission(GuildPermission.Administrator)]
        public async Task TaskProxyMessage([Remainder] string userInput, ISocketMessageChannel channel)
        {
            await Context.Channel.SendFileAsync(userInput, "Caption goes here");
        }
    }
}
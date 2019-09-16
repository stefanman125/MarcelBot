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
    public class CheckRep : ModuleBase<SocketCommandContext>
    {
        [Command("CheckRep"), Alias("checkrep", "Checkrep", "checkRep"), Summary("Checks current rep")]
        public async Task TaskCheckRep()
        {
            /*var csvContents = File.ReadAllText("repValues.csv").Split('\n');
            var csvArray = from line in csvContents select line.Split(',').ToArray();
            foreach (var item in csvArray)
            {
                //item[0] would be the userID, [1] would be the timesHelped, [2] would be the resourcesProvided, [3] would be xp, [4] would be rank
                if (item[0] == (Context.User.Username + "#" + Context.User.Discriminator))
                {


                    Random genRandom = new Random();
                    EmbedBuilder Embed = new EmbedBuilder();
                    Embed.WithAuthor(Context.User);
                    Embed.WithColor(genRandom.Next(1, 255), genRandom.Next(1, 255), genRandom.Next(1, 255));
                    Embed.WithDescription("An overview of your stats in this server");
                    Embed.AddField(":handshake: Times you've helped someone out:","```js\n1```");
                    Embed.AddField(":books: Times you've provided resources:","```js\n2```");
                    Embed.AddField(":medal: Experience:","```Level: 2 (150/200) | 75% -> Level 3```");
                    Embed.AddField("Current Rank:","Occcasional Helper");
                    Embed.WithImageUrl("https://d1d7x9j9v0pvmk.cloudfront.net/2018/02/Kurzgesagt_Showreel_CLIENT_STILL-715x402.jpg");
                    Embed.WithFooter("" + DateTime.Now);
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());
                }
                else
                {
                    CreateRepRecord(Context.User.Username + "#" + Context.User.Discriminator);
                }
            }

            string CreateRepRecord(string userName)
            {
                return userName;
            }
            */
        }
    }
}
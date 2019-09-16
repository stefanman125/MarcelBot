using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
//Request Library
using System.Net;
using System.IO;

namespace MarcelBot.Core.Commands
{
    public class Joke : ModuleBase<SocketCommandContext>
    {
        [Command("Joke"), Alias("joke"), Summary("Uses the icanhazdadjoke api to fetch a dad joke")]
        public async Task TaskJoke()
        {
            string html = string.Empty;
            string url = @"https://icanhazdadjoke.com/";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            string joke = "";
            string[] array = html.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (String item in array)
                if (item.StartsWith("<p class="+'"'+"subtitle"+'"'+">"))
                {
                    joke = item.Substring(20, (item.Length-24));
                }
            joke = joke.Replace("<br>", "");

            await Context.Channel.SendMessageAsync(joke);
        }
    }
}
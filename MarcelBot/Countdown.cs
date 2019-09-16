using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace MarcelBot.Core.Commands
{
    public class Countdown : ModuleBase<SocketCommandContext>
    {
        [Command("countdown"), Alias("Countdown", " countdown", " Countdown"), Summary("Tells how much time is left before the end of the semester")]
        public async Task TaskCountdown()
        {
            //The year-month-day of the end date
            DateTime dateStartOfFallSemester = new System.DateTime(2019, 09, 03);
            await Context.Channel.SendMessageAsync(GetDaysLeft(dateStartOfFallSemester, "start of the fall semester!"));

            DateTime dateEndOfFallSemester = new System.DateTime(2019, 12, 13);
            await Context.Channel.SendMessageAsync(GetDaysLeft(dateEndOfFallSemester, "end of the fall semester!"));
        }

        //Returns a string saying how many days left until whatever date
        public String GetDaysLeft(DateTime date, String description) 
        {
            TimeSpan daysLeft = date.Subtract(DateTime.Now);
            if (Convert.ToInt32(daysLeft.Days) <= 0)
            {
                return ("It's the " + description + "!");
            }
            else
            {
               return (daysLeft.Days + " days left until the " + description + "!");
            } 
        }
    }
}
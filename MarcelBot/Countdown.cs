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

            //Start of the Winter Semester
            DateTime dateStartOfWinterSemester = new System.DateTime(2020, 1, 6);
            if (DateTime.Today <= dateStartOfWinterSemester)
            {
                await Context.Channel.SendMessageAsync(GetDaysLeft(dateStartOfWinterSemester, "start of the winter semester!"));
            }

            //End of the Winter Semester
            DateTime dateEndOfWinterSemester = new System.DateTime(2020, 4, 17);
            if (DateTime.Today <= dateEndOfWinterSemester && DateTime.Today > dateStartOfWinterSemester)
            {
                await Context.Channel.SendMessageAsync(GetDaysLeft(dateEndOfWinterSemester, "end of the winter semester!"));
            }

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
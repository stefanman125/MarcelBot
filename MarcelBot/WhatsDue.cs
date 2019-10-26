using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace MarcelBot.Core.Commands
{
    public class WhatsDue : ModuleBase<SocketCommandContext>
    {
        [Command("whatsdue"), Alias("whats due", "what is due"), Summary("Shows the current work that is yet to be due, and reminds you that its due")]
        public async Task TaskWhatsDue([Remainder]string remainder = "")
        {
            remainder.ToLower();
            /*
            //Cast Context user as a SocketGuildUser so that I can reference their roles
            var user = Context.User as SocketGuildUser;

            //This will grab persistent values from the file
            List<string> listWork = System.IO.File.ReadLines("WorkList.txt").ToList();

            //If the user wants to use an argument
            if (user.Roles.Any(r => r.Name == "Robot"))
            {
                var listRemainder = remainder.Split('(', ')', ',');
                var lengthListOriginal = listWork.Count();

                //If the user wants to add something to the work list
                if (listRemainder[0].Contains("addwork"))
                {
                    listWork.Add(listRemainder[1] + " | " + listRemainder[2] + listRemainder[3] + " at " + listRemainder[4]);
                    System.IO.File.WriteAllLines("WorkList.txt", listWork);
                    await Context.Channel.SendMessageAsync("Work has been successfully added!");
                }

                //If the user wants to remove work from the list
                else if (listRemainder[0].Contains("removework"))
                {
                    //Make sure the user enters a number to remove the work
                    try { Convert.ToInt32(listRemainder[1]); }
                    catch (FormatException) { await Context.Channel.SendMessageAsync("Error: Please enter the number of the work you want to remove."); }
                    //Make sure the number is within the lists range
                    try { var test = listWork[Convert.ToInt32(listRemainder[1]) - 1]; }
                    catch (ArgumentOutOfRangeException) { await Context.Channel.SendMessageAsync("Error: The number provided does not exist in the current list, or the list is empty."); }

                    foreach (string item in listWork)
                    {
                        //If the index of the item is equal to the users number -1 because of padding on the list
                        if (listWork.IndexOf(item) == (Convert.ToInt32(listRemainder[1]) - 1))
                        {
                            listWork.Remove(item);
                            await Context.Channel.SendMessageAsync("Work was successfully removed!");
                            System.IO.File.WriteAllLines("WorkList.txt", listWork);
                            break;
                        }
                    }
                }

                //If the user wants to edit some work
                else if (listRemainder[0].Contains("editwork"))
                {
                    //Make sure the user enters a number to remove the work
                    try { Convert.ToInt32(listRemainder[1]); }
                    catch (FormatException) { await Context.Channel.SendMessageAsync("Error: Please enter the number of the work you want to edit."); }

                    try
                    {
                        //Convert date into array of year, month, day
                        var formattedDate = listRemainder[4].Split('-');

                        // ----- This entire block just changes the language based on the date (i.e 27th, 1st, 2nd, this day, today, February, etc) -----
                        DateTime dateWorkDue = new System.DateTime(Convert.ToInt32(DateTime.Today.Year), Convert.ToInt32(formattedDate[0]), Convert.ToInt32(formattedDate[1]));
                        TimeSpan daysLeft = dateWorkDue.Subtract(DateTime.Today);

                        //If date is today
                        if (daysLeft.Days == 0) { listRemainder[4] = " due today"; }
                        //If date is tomorrow
                        if (daysLeft.Days == 1) { listRemainder[4] = " due tomorrow"; }
                        //If the date is less than 7 days
                        else if (daysLeft.Days < 7) { listRemainder[4] = " due this " + dateWorkDue.DayOfWeek; }
                        //If the date is neither of the above
                        else
                        {
                            if (listRemainder[4].Substring(listRemainder[4].Count() - 1, 1) == "1") { formattedDate[1] += "st"; }
                            else if (listRemainder[4].Substring(listRemainder[4].Count() - 1, 1) == "2") { formattedDate[1] += "nd"; }
                            else if (listRemainder[4].Substring(listRemainder[4].Count() - 1, 1) == "3") { formattedDate[1] += "rd"; }
                            else { formattedDate[1] += "th"; }
                            listRemainder[4] = " due on the " + formattedDate[1].Replace("0", String.Empty) + " of " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(formattedDate[0]));
                        }
                        // -------------------------------------------------------------------------------------------------------------------------------

                        //Change the item of the work list that has the index of the users input -1 because the index of the printed list starts at 1
                        listWork[Convert.ToInt32(listRemainder[1]) - 1] = listRemainder[2] + " | " + listRemainder[3] + listRemainder[4] + " at " + listRemainder[5];
                        System.IO.File.WriteAllLines("WorkList.txt", listWork);
                        await Context.Channel.SendMessageAsync("Work was successfully edited!");
                    }
                    catch (IndexOutOfRangeException)
                    {
                        await Context.Channel.SendMessageAsync("Error: Work number: " + listRemainder[1] + "does not exist in the list.\nExample: `EditWork(Number,Course,Title,Date,Time)`");
                    }
                }

                //Print the help list if the Robot didn't enter any correct commands
                else if (remainder != "")
                {
                    EmbedBuilder Embed = new EmbedBuilder();
                    Embed.WithColor(0, 181, 247);
                    Embed.WithTitle("WhatsDue argument list");
                    Embed.WithDescription(
                        "`AddWork`: Add work to the list\n" +
                        "`EditWork`: Edit one of the items in the list\n" +
                        "`RemoveWork`: Remove an item from the list\n" +
                        "\nLeave no arguments after WhatsDue to just print the list.");
                    Embed.WithFooter("" + DateTime.Now);
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());
                }

                //Send the list if a robot doesn't want to use any arguments 
                else { await FuncSendWorkList(); }
            }

            //If a non-robot uses the command, send the work list
            else if (user.Roles.Any(r => r.Name != "Robot")) { await FuncSendWorkList(); }

            //Function that sends the list of work if the user using the command is not Robot
            async Task FuncSendWorkList()
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithColor(0, 181, 247);

                //If list has something, change embed title
                if (listWork.Count != 0)
                {
                    string embedDescription = "";
                    foreach (string item in listWork)
                    {
                        embedDescription += listWork.IndexOf(item) + 1 + " | " + item + "\n";
                    }
                    Embed.WithTitle("A list for upcoming work");
                    Embed.WithDescription(embedDescription);
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());
                }
                //If list has nothing, change embed title
                else
                {
                    Embed.WithTitle("There is currently no work to be done!");
                    Embed.WithDescription("");
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());
                }
            }


            // Useful code for if I decide to make a remindme function
            /*This will grab usernames from a file to know who to remind
            List<Discord.WebSocket.SocketUser> listUsers = new List<Discord.WebSocket.SocketUser>();

            Function that sends reminder to all people subscribed
            async Task FuncSendReminder()
            {
                var userID = Context.Message.Author;
                await Discord.UserExtensions.SendMessageAsync(userID, "Yeet");
            }*/
        }//
    }
}
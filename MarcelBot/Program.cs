using System;
using System.Reflection;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace ChrisRobertson
{
    class Program
    { 
        private DiscordSocketClient Client;
        private CommandService Commands;

        static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });

            Commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = false,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
                
            });

            Client.MessageReceived += Client_MessageRecieved;
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly());

            Client.Ready += Client_Ready;
            Client.Log += Client_Log;

            //Change this whenever creating new bots -----------------------------------------------------
            //Use Chris Robertson's token when testing: NDMwNDgxNjg5MjUzMzgwMTA2.DvSxWQ.-hb0VUo0oOEAj1lEb5PDplqpJ3Q
            //Marcel's original token: NTIyOTU2OTA3ODEyNDg3MTg5.DvSjUg.tbXjmSSoZWN_XMKvyp8PNn3-xUE
            string Token = "NTIyOTU2OTA3ODEyNDg3MTg5.DvSjUg.tbXjmSSoZWN_XMKvyp8PNn3-xUE";
            //Change this whenever creating new bots -----------------------------------------------------
            System.IO.File.AppendAllText(@"log.txt", $"---- NEW INSTANTIATION AT {DateTime.Now} ---- "+Environment.NewLine);
            await Client.LoginAsync(TokenType.Bot, Token);
            await Client.StartAsync();

            await Task.Delay(-1);
        }

        //This creates sort of a baseline of all the logs
        private async Task Client_Log(LogMessage Message)
        {
            var log = $"{DateTime.Now} at {Message.Source}] {Message.Message}";
            Console.WriteLine(log);
            System.IO.File.AppendAllText(@"log.txt", log+Environment.NewLine);
        }

        private async Task Client_Ready()
        {
            //Shows what Bot is doing
            await Client.SetGameAsync("with your Python code", "https://www.Google.com", StreamType.NotStreaming);
        }

        private async Task Client_MessageRecieved(SocketMessage MessageParam)
        {
            var Message = MessageParam as SocketUserMessage;
            var Context = new SocketCommandContext(Client, Message);

            var logMessage = $"{DateTime.Now} at Messages] {Message.Author} | {Message.Content}";
            Console.WriteLine(logMessage);
            System.IO.File.AppendAllText(@"log.txt", logMessage + Environment.NewLine);

            if (Context.Message == null || Context.Message.Content == "") return;
            if (Context.User.IsBot) return;

            int ArgPos = 0;

            if (!(Message.HasStringPrefix("Marcel.", ref ArgPos) || Message.HasMentionPrefix(Client.CurrentUser, ref ArgPos))) return;

            //So here's where im at rn. I can sanitize the message content with the lines below, but cant actually change Context values cuz its read only
            string msg = Context.Message.Content;
            msg = msg.Replace(" ", string.Empty);

            Console.WriteLine(Context);
            var Result = await Commands.ExecuteAsync(Context, ArgPos);
            //If the command was not successful
            if (!Result.IsSuccess)
            {
                var log = $"{DateTime.Now} at Commands] Something went wrong with executing a command | {Context.User.Username} tried to execute a command: {Context.Message.Content} | Error: {Result.ErrorReason}";
                Console.WriteLine(log);
                System.IO.File.AppendAllText(@"log.txt", log + Environment.NewLine);
            }
            //If the command was successful
            else if (Result.IsSuccess)
            {
                var log = $"{DateTime.Now} at Commands] User {Context.User.Username} executed a command: {Context.Message.Content}";
                //Truncates all logs that are related to heartbeats to reduce size
                if (!log.Contains("Heartbeat")) { Console.WriteLine(log); }
                System.IO.File.AppendAllText(@"log.txt", log + Environment.NewLine);
            }
        }
    }
}

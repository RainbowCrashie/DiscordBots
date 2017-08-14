using System;
using System.Threading.Tasks;
using Discord;
using DiscordNetPlus;

namespace TrixieDiscord
{
    public class Program
    {
        public DiscordSocketClientPlus Client { get; set; }

        public static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            var token = Properties.Settings.Default.Token;
            if (token == "")
            {
                Console.Write("Paste token: ");
                token = Console.ReadLine();
                Properties.Settings.Default.Token = token;
                Properties.Settings.Default.Save();
            }

            Client = new DiscordSocketClientPlus(token);
            await Client.Start();

            await Task.Delay(-1);
        }

        public async void HeartBeat()
        {
            while (true)
            {
                await Task.Delay(new TimeSpan(0, 5, 0));

                if (Client?.LoginState != LoginState.LoggedIn)
                    continue;
                
                await Client.SetGameAsync($" ");
            }
        }
    }
}

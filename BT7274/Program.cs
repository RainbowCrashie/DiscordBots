using System;
using System.Threading.Tasks;
using BT7274.DataServices;
using BT7274.MilitiaHeadquarters;
using Discord;
using DiscordNetPlus;
using DiscordNetPlus.Utils;

namespace BT7274
{
    public class Program
    {
        public DiscordSocketClientPlus Client { get; set; }

        public static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        private void Initialize()
        {
            Locator.LoadoutData = new LoadoutData();
            Locator.LoadoutData.Load();
        }

        public async Task MainAsync()
        {
            Initialize();

            HeartBeat();

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
                
                await Client.SetGameAsync($"Upgrade Core '{(int)Uptime.TimeSpan.TotalMinutes / 5 + 1}");
                await Client.LogConsole(new LogMessage(LogSeverity.Debug, "HeartBeat", Uptime.Print));
            }
        }
    }
}

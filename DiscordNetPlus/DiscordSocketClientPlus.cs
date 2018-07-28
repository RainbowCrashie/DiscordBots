using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordNetPlus.Commands;
using DiscordNetPlus.Interaction;
using DiscordNetPlus.Properties;

namespace DiscordNetPlus
{
    public class DiscordSocketClientPlus : DiscordSocketClient
    {
        private string Token { get; }
        public CommandHandler CommandHandler { get; set; }
        public RelationMemory RelationMemory { get; set; }

        public DiscordSocketClientPlus(string token) : base(new DiscordSocketConfig {LogLevel = LogSeverity.Debug })
        {
            Token = token;
        }

        public async Task Start()
        {
            Log += LogConsole;

            await LoginAsync(TokenType.Bot, Token);
            await StartAsync();

            CommandHandler = new CommandHandler(this);
            await CommandHandler.InitializeAsync();

            RelationMemory = new RelationMemory();
            
            await Task.Delay(5000);
            await SetGameAsync(Settings.Default.PlayngGame);
        }

        public async Task Stop()
        {
            Log -= LogConsole;
            await StopAsync();
        }

        public async Task Restart()
        {
            await Stop();
            await Start();
        }

        public Task LogConsole(LogMessage msg)
        {
            if (msg.Severity == LogSeverity.Debug)
                Console.ForegroundColor = ConsoleColor.DarkGray;

            if (msg.Source == CommandBase.CommandLogSourceName)
                Console.ForegroundColor = ConsoleColor.Gray;

            Console.Write($"[{DateTime.Now:T}] ");

            if (msg.Severity == LogSeverity.Critical ||
                msg.Severity == LogSeverity.Error ||
                msg.Severity == LogSeverity.Warning)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"[{msg.Severity}] ");
            }

            Console.Write($"[{msg.Source}]\t{msg.Message}{Environment.NewLine}");

            Console.ForegroundColor = ConsoleColor.White;

            return Task.CompletedTask;
        }


    }

    public class MonaSocketCommandContext : SocketCommandContext
    {
        public new DiscordSocketClientPlus Client { get; }

        public MonaSocketCommandContext(DiscordSocketClientPlus client, SocketUserMessage msg) : base(client, msg)
        {
            Client = client;
        }
    }
}
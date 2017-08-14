using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using DiscordNetPlus.Extentions;

namespace DiscordNetPlus
{
    public class CommandHandler
    {
        #region Fields and Properties

        private DiscordSocketClientPlus Client { get; }
        public CommandService CommandService { get; }

        #endregion

        #region Ctor
        public CommandHandler(DiscordSocketClientPlus client)
        {
            Client = client;
            
            CommandService = new CommandService();
        }
        #endregion

        #region Methods
        public async Task InitializeAsync()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.ManifestModule.Name != "DiscordNetPlus.dll")
                    continue;

                await CommandService.AddModulesAsync(assembly);
            }
            await CommandService.AddModulesAsync(Assembly.GetEntryAssembly());

            Client.MessageReceived += HandleCommandAsync;
        }

        public async Task HandleCommandAsync(SocketMessage s)
        {
            var message = s as SocketUserMessage;
            if (message == null)
                return;

            var context = new MonaSocketCommandContext(Client, message);

            var prefix = Properties.Settings.Default.CommandPrefix;

#if DEBUG
            prefix = Properties.Settings.Default.DebugCommandPrefix;
#endif

            var argPosition = 0;
            if (message.HasCharPrefix(prefix, ref argPosition))
            {
                var result = await CommandService.ExecuteAsync(context, argPosition);

                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    var r = (ExecuteResult)result;
                    await context.Channel.SendMessageAsync(r.Exception.Message.SintaxHighlightingMultiLine());
                    await context.Log(r.ToString());
                }
            }
        }
        #endregion

    }
}

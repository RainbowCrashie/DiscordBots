using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using DiscordNetPlus.Commands;

namespace DiscordNetPlus.Extentions
{
    public static class DiscordExtentions
    {
        private const string CodeFormattingMultiLine = "```";
        public static string SintaxHighlightingMultiLine(this string str, string language = "")
        {
            return $"{CodeFormattingMultiLine}{language}{Environment.NewLine}{str}{Environment.NewLine}{CodeFormattingMultiLine}";
        }

        private const string CodeFormatting = "`";
        public static string SintaxHighlighted(this string str, string language = "")
        {
            return $"{CodeFormatting}{language}{Environment.NewLine}{str}{Environment.NewLine}{CodeFormatting}";
        }

        public static async Task Log(this MonaSocketCommandContext context, string message)
        {
            await context.Client.LogConsole(new LogMessage(LogSeverity.Debug, CommandBase.CommandLogSourceName, message));
#if (!DEBUG)
            return;
#endif
            const int chunkSize = 1000;
            const int interval = 3000;

            var chunks = message.DivideString(chunkSize).ToList();
            foreach (var chunk in chunks)
            {
                var index = "";
                if (chunks.Count > 1)
                    index = $"[{chunks.IndexOf(chunk) + 1}/{chunks.Count}]{Environment.NewLine}";

                await context.Channel.SendMessageAsync($"{index}{chunk}".SintaxHighlightingMultiLine());
                await Task.Delay(interval);
            }
        }

    }
}
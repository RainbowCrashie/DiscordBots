using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using DiscordNetPlus.Extentions;

namespace DiscordNetPlus.Commands
{
    public class CommandBase : ModuleBase<MonaSocketCommandContext>
    {
        #region Fields and Properties

        protected static Color EmbedColor = new Color(31, 151, 242);

        protected IDisposable TypingIndicator => Context.Channel.EnterTypingState();
        private Stopwatch Stopwatch { get; } = new Stopwatch();
        
        public static string CommandLogSourceName { get; } = "Command Execution";

        #endregion
        
        protected void StartStopwatch()
        {
            Stopwatch.Reset();
            Stopwatch.Start();
        }
        
        protected async Task<long> RecordStopwatch(string caption = "", bool continueIt = true)
        {
            Stopwatch.Stop();

            var duration = Stopwatch.ElapsedMilliseconds;
            var message = $"{caption}: {Stopwatch.ElapsedMilliseconds}ms";

            await Context.Log(message);

            if (continueIt)
                StartStopwatch();

            return duration;
        }

        #region Replying Methods
        protected async Task<RestUserMessage> ReplyEmbed(Embed embed, string text = "")
        {
            return await Context.Channel.SendMessageAsync(text, false, embed);
        }

        protected async Task ReplyVolatile(string text, int durationSeconds = 10)
        {
            var message = await Context.Channel.SendMessageAsync(text);
            await Task.Delay(durationSeconds * 1000);
            await message.DeleteAsync();
        }

        protected async Task<IUserMessage> DMReplyEmbed(Embed embed, string text = "")
        {
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            return await dmChannel.SendMessageAsync(text, false, embed);
        }

        protected static EmbedBuilder EmbedBuilderFactory()
        {
            var embed = new EmbedBuilder();
            embed.Color = EmbedColor; //change it to loadAppSetting

            return embed;
        }
        #endregion

    }
    
    //TODO: Create server specific data storing when I wish.
    public class GuildCommandBase : CommandBase
    {
        protected SocketVoiceChannel ConnectedVoiceChannel()
        {
            var user = Context.User as SocketGuildUser;
            OperatedOnGuildValidation(user);

            var channel = user.VoiceChannel;
            VoiceChannelConnectedValidation(channel);

            return channel;
        }

        protected static void OperatedOnGuildValidation(SocketGuildUser user)
        {
            if (user == null)
                throw new InvalidOperationException("Command not called on a server");
        }

        protected static void VoiceChannelConnectedValidation(SocketVoiceChannel channel)
        {
            if (channel == null)
                throw new InvalidOperationException("You are not connected to a voice channel");
        }
    }
}
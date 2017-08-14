using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordNetPlus.Utils;

namespace DiscordNetPlus.Commands
{
    public class InfoCommand : CommandBase
    {
        [Command("info")]
        public async Task Info()
        {
            var embed = new EmbedBuilder
            {
                Author = new EmbedAuthorBuilder
                {
                    Name = Context.Client.CurrentUser.Username,
                    IconUrl = Context.Client.CurrentUser.GetAvatarUrl()
                },
                Url = "https://discord.foxbot.me/docs/",
                Color = EmbedColor
            };

            embed.AddInlineField("Version", Assembly.GetExecutingAssembly().GetName().Version.ToString());
            embed.AddInlineField("Library", "[`Discord.Net`](https://discord.foxbot.me/docs/)");
            embed.AddInlineField("Running on", "Azure App Services");
            
            embed.AddInlineField("Author", "mo");
            embed.AddInlineField(".NET Version", Environment.Version);
            embed.AddInlineField("Uptime", Uptime.Print);
            
            await ReplyEmbed(embed);
        }

        [Command("userinfo")]
        public async Task UserInfo(IGuildUser usr = null)
        {
            var user = usr as SocketGuildUser;
            if (usr == null) user = Context.User as SocketGuildUser;

            if (user == null)
                throw new NullReferenceException();

            var embed = new EmbedBuilder
            {
                Author = new EmbedAuthorBuilder
                {
                    Name = $"{user.Username}#{user.Discriminator}",
                    IconUrl = user.GetAvatarUrl()
                },
                Color = EmbedColor,
                ThumbnailUrl = user.GetAvatarUrl()
            };
            
            embed.AddInlineField("ID", user.Id);
            embed.AddInlineField("Status", user.Status);

            embed.AddInlineField("Mention", user.Mention);
            embed.AddInlineField("Playing", user.Game ?? new Game("nullpo", "", StreamType.NotStreaming));

            var joined = DateTimeOffset.UtcNow - user.CreatedAt;
            embed.AddField("Joined",
                $"{(int)joined.TotalDays} Days {joined.Hours} Hours {joined.Minutes} Minutes Ago " +
                $"({user.CreatedAt:yyyy.M.d h:m:s UTC})");

            embed.AddField("Roles",
                string.Join(", ", user.Roles.Where(r => r.Name != "@everyone"))
            );

            await ReplyEmbed(embed);
        }
    }
}
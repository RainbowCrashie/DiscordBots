using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordNetPlus.Commands;

namespace TrixieDiscord.Commands
{
    public class PonystreamCommand : CommandBase
    {
        [Command("stream")]
        public async Task NotifyStream()
        {
            var embed = new EmbedBuilder
            {
                Author = new EmbedAuthorBuilder
                {
                    Name = "MLPリアルタイム視聴リンク",
                    IconUrl = Context.Client.CurrentUser.GetAvatarUrl()
                },
                Url = "http://www.ustream.tv/channel/ponyspazz",
                Color = EmbedColor
            };

            embed.AddField("Stream URL", ".　　**最高画質**: http://www.ustream.tv/channel/ponyspazz" + Environment.NewLine +
                                     ".　　[バックアップ(低画質、透かしあり) - ↑が稀に落ちるので](http://www.ustream.tv/channel/brst20131023)");
            embed.AddField("AdBlock", ".　　視聴中広告が出てしまうため、広告ブロックツールの導入を推奨します。"+ Environment.NewLine +
                                      ".　　[**`Chrome`**](https://chrome.google.com/webstore/detail/ublock-origin/cjpalhdlnbpafiamejdnhcphjbkeiagm) [**`FireFox`**](https://addons.mozilla.org/en-us/firefox/addon/ublock-origin/)");
            
            await ReplyEmbed(embed);
        }
    }
}
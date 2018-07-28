using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DiscordNetPlus.Commands
{
    public class HelpCommand : CommandBase
    {
        const string ReplyToChannelFlag = "channel";

        [Command("help")]
        public async Task Help(string replyToChannelFlag = "")
        {
            var embed = new EmbedBuilder
            {
                Author = new EmbedAuthorBuilder
                {
                    Name = Context.Client.CurrentUser.Username + " Commands",
                    IconUrl = Context.Client.CurrentUser.GetAvatarUrl()
                },
                Url = "https://github.com/RainbowCrashie/DiscordBots",
                Color = EmbedColor
            };


            embed.AddField("みかた", "!コマンド {!省略形}　(必須引数) <任意引数:デフォルト値>"
                + Environment.NewLine + "　　　　　　　:");

            embed.AddField("!info",
                "このbotの詳細を表示します"
                + Environment.NewLine + "　　　　　　　:");

            embed.AddField("!userinfo <@user>",
                "@で指定したユーザーの情報を表示します。空の場合はコマンド送信者の情報を表示します"
                + Environment.NewLine + "　　　　　　　:");

            embed.AddField("!split {!st}　<チーム数:2>",
                "接続中のチャンネルにいるプレイヤーを、ランダムに二分します。3つ以上のチーム数も指定できます。");

            embed.AddField("!split assign {!st a}",
                "splitで分けたチーム編成を元に、プレイヤーをボイスチャンネルに振り分けます。");

            embed.AddField("!split recall {!st r}",
                "split assignで振り分けたプレイヤーを元のボイスチャンネルに呼び戻します。"
                 + Environment.NewLine + "　　　　　　　:");

            embed.AddField("!pleaseEmbed {!plzemb}　(リンクURL) (画像URL)",
                "URLと画像がセットになってる情報ボックスを表示します"
                 + Environment.NewLine + "　　　　　　　:");

            embed.AddField("!じゃんけん {!rps}　(グー/チョキ/パー)",
                "botとじゃんけん。絵文字等一通り反応します　グー ぐー rock :fist :, ✊"
                 + Environment.NewLine + "　　　　　　　:");

            embed.AddField("!trixie {!t !trix !トリクシー}",
                "ランダムなトリクシー画像を表示します"
                 + Environment.NewLine + "　　　　　　　:");

            embed.AddField("!derpibooru {!dpb}　(query)",
                "ランダムなポニー画像を表示します"
                 + Environment.NewLine + "　　　　　　　:");


            if (replyToChannelFlag == ReplyToChannelFlag)
                await ReplyEmbed(embed);
            else
                await DMReplyEmbed(embed);
        }
    }
}
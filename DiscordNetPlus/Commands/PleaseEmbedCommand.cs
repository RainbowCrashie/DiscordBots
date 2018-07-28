using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordNetPlus.Extentions;
using Discord.Net;

namespace DiscordNetPlus.Commands
{
    [Group("plzemb"), Alias("pleaseEmbed")]
    public class PleaseEmbedCommand : CommandBase
    {
        [Command("help")]
        public async Task Help()
        {
            await ReplyAsync("```リンクと画像のembedを作為的に作成します" + Environment.NewLine +
                             "Usage:　　!plzemb　ソース元URL(dA/pixivなど)　貼りたい画像の直リンク```");
        }

        [Command(""), Summary("リンクと画像のembedを作為的に作成します")]
        public async Task PleaseEmbed([Summary("ソース元URL(dA/pixivなど)")]string sourceUrl, [Summary("貼りたい画像の直リンク")]string imageUrl)
        {
            try
            {
                await Context.Message.DeleteAsync();
            }
            catch (HttpException e)
            {
                if (!e.Message.Contains("403"))
                    throw;

                await ReplyAsync("「メッセージの管理(Manage Messages)」権限未所持のため、送信元のチャットが削除できず、内容が2重になっている可能性があります。".SintaxHighlightingMultiLine());
            }

            //TODO:添付をアップロード
            if (imageUrl == null && Context.Message.Attachments.Count == 0)
            {
                await ReplyVolatile("表示したい画像を指定してください");
                return;
            }

            var embed = new EmbedBuilder();

            embed.Author = new EmbedAuthorBuilder
            {
                Name = $"{Context.User.Username} さんが共有しました: ",
                IconUrl = Context.User.GetAvatarUrl()
            };
            embed.Title = sourceUrl;
            embed.Url = sourceUrl;
            embed.ImageUrl = imageUrl;

            await ReplyEmbed(embed);
        }
    }
}
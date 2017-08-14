using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DiscordNetPlus.Commands
{
    [Group("plzemb"), Alias("pleaseembed")]
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
            await Context.Message.DeleteAsync();

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
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DiscordNetPlus.Commands
{
    [Group("trixie"), Alias("t", "trix", "トリクシー")]
    public class TrixieCommand : RandomDpbCommandBase
    {
        private readonly string[] _queryElements = 
        {
            "trixie",
            "!(anthro||human||comic||humanized||source filmmaker)",
            "aspect_ratio.gt:0.8&&aspect_ratio.lt:1.8"
        };
        private const int ScoreMinimumDefault = 20;

        private string Query => string.Join("&&", _queryElements);

        [Command("")]
        public async Task Trixie(int score = ScoreMinimumDefault)
        {
            await ReplyRandomDerpibooru($"{Query}");
        }

        [Command("q")]
        public async Task Q()
        {
            await ReplyAsync($"{Query}");
        }
    }

    public class DpbCommand : RandomDpbCommandBase
    {
        [Command("derpibooru"), Alias("dpb")]
        public async Task Dpb(string query)
        {
            await ReplyRandomDerpibooru(query);
        }
    }

    public class SayCommand : CommandBase
    {
        [Command("say")]
        public async Task Say(string text, ITextChannel channel = null)
        {
            if (channel == null)
                channel = (ITextChannel)Context.Channel;

            await Context.Message.DeleteAsync();
            await channel.SendMessageAsync(text);
        }
    }

    public class NullpoCommand : CommandBase
    {
        [Command("ぬるぽ"), Summary("ｶﾞｯ")]
        public async Task Nullpo()
        {
            await ReplyAsync("ｶﾞｯ");
        }
    }

    public class PingCommand : CommandBase
    {
        [Command("ping")]
        public async Task Ping()
        {
            await ReplyAsync("Pong.");
        }
    }

}
using System.Threading.Tasks;
using Discord.Commands;
using DiscordNetPlus.Interaction;

namespace DiscordNetPlus.Commands
{
    [Group("agr")]
    public class MemorialAggregateCommand : CommandBase
    {
        [Command("start")]
        public async Task Start(int number = 0)
        {
            var conv = new MaConv();
            conv.CurrentNumber = number;
            Context.Client.RelationMemory.Memorise(Context.Channel, conv);
            await ReplyAsync($"アグリゲータモジュールを起動、初期値: `{number}`");
        }

        [Command("add"), Alias("", "+")]
        public async Task Add(int number)
        {
            var conv = Context.Client.RelationMemory.Recall(Context.Channel, new MaConv().Topic);
            var maConv = (MaConv)conv;
            maConv.CurrentNumber += number;
            await ReplyAsync($"追加しました。現在値: `{maConv.CurrentNumber}`");
        }

        private class MaConv : IConversation
        {
            public object Topic { get; set; } = nameof(MemorialAggregateCommand);
            public int CurrentNumber { get; set; }
        }
    }
}
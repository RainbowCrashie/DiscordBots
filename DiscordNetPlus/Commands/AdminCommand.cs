using System;
using System.Threading.Tasks;
using Discord.Commands;
using DiscordNetPlus.Properties;

namespace DiscordNetPlus.Commands
{
    [Group("admin"), RequireOwner]
    public class AdminCommand : CommandBase
    {
        [Command("stop")]
        public async Task Stop()
        {
            await ReplyAsync("```[CRITICAL] 動力低下。動力が足りません。全感覚への電源供給を停止中...```");
            Environment.Exit(0);
        }

        [Command("restart")]
        public async Task Restart()
        {
            await ReplyAsync("```[警告] システムに問題が発生。再起動を試みます。```");

            StartStopwatch();
            await Context.Client.Restart();

            var elapsed = RecordStopwatch();
            await ReplyAsync($"```[INFO] システムの再起動に成功。所要時間: {elapsed}```");

        }

        [Command("setgame")]
        public async Task SetGame(string label)
        {
            Settings.Default.PlayngGame = label;
            Settings.Default.Save();
            await Context.Client.SetGameAsync(label);
            await Context.Client.SetGameAsync(label);
        }
    }
}
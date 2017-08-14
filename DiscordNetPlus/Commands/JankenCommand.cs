using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;

namespace DiscordNetPlus.Commands
{
    public class JankenCommand : CommandBase
    {
       #region Methods
        [Command("rps"), Description("じゃんけん")]
        public async Task Janken([Summary("グー/チョキ/パー")]string te)
        {
            var userTe = Te.Parse(te);

            var ran = new Random();
            var wl = ran.Next(2);

            Te btTe;
            string message;
            if (wl == 0)
            {
                btTe = userTe.WeakerAgainst;
                message = "負け";
            }
            else
            {
                btTe = userTe.StrongerAgainst;
                message = "勝ち";
            }

            await ReplyAsync($"{Context.User.Mention}　{btTe.Emote} あなたの{message}です、パイロット。");
        }
        #endregion
    }

    public class Te
    {
        public string[] Aliases { get; }
        public string Emote { get; }
        public Te StrongerAgainst { get; set; }
        public Te WeakerAgainst { get; set; }
        
        protected Te(string[] aliases, string emote)
        {
            Aliases = aliases;
            Emote = emote;
        }

        public static Te Parse(string raw)
        {
            raw = raw.Replace(" ", "");
            raw = raw.Replace("　", "");

            if (Rock.Aliases.Contains(raw))
                return Rock;
            if (Scissors.Aliases.Contains(raw))
                return Scissors;

            return Paper;
        }

        public static Te Rock { get; } = new Te(new[] {"Rock", "グー", "ぐー", ":fist:", "✊"}, ":fist:");
        public static Te Scissors { get; } = new Te(new[] {"Scissors", "ちょき", "チョキ", ":v:", "✌" }, ":v:");
        public static Te Paper { get; } = new Te(new []{"Paper", "パー", "ぱー", ":hand_splayed:", "🖐" }, ":hand_splayed:");

        static Te()
        {
            Rock.WeakerAgainst = Paper;
            Rock.StrongerAgainst = Scissors;

            Scissors.WeakerAgainst = Rock;
            Scissors.StrongerAgainst = Paper;

            Paper.WeakerAgainst = Scissors;
            Paper.StrongerAgainst = Rock;
        }
    }
}
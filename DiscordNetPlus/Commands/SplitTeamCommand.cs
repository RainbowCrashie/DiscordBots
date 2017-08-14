using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using DiscordNetPlus.Extentions;

namespace DiscordNetPlus.Commands
{
    public class SplitTeamCommand : GuildCommandBase
    {
        [Command("split"), Alias("st"), RequireContext(ContextType.Guild)]
        public async Task SplitTeam(int teamNumber = 2)
        {
            var channelmembers = ConnectedVoiceChannel().Users;
            var result = Split(channelmembers, teamNumber);

            var embed = EmbedBuilderFactory();

            var counter = 1;
            foreach (var team in result)
            {
                embed.AddField($"Team #{counter++}", string.Join(", ", team.Select(user => user.Nickname)));
            }

            await ReplyEmbed(embed, $"Splitted {channelmembers.Count} members into {teamNumber} groups.");
        }

        private static IEnumerable<IEnumerable<T>> Split<T>(IEnumerable<T> sourceList, int teamNumber)
        {
            var list = sourceList.ToList();

            var rnd = new Random(); //Declaring this outside as a field will make this method thread-unsafe.
            var shuffledList = list.OrderBy(member => rnd.Next()).ToList(); //note: result will duplicate when called at the same time. 

            var memberPerTeam = list.Count / teamNumber;

            var result = shuffledList.SplitList(memberPerTeam);

            return result;
        }
    }
}
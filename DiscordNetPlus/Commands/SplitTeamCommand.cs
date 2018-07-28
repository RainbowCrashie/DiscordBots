using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using DiscordNetPlus.Extentions;
using DiscordNetPlus.Interaction;

namespace DiscordNetPlus.Commands
{
    [Group("split"), Alias("st"), RequireContext(ContextType.Guild)]
    public class SplitTeamCommand : GuildCommandBase
    {
        [Command("")]
        public async Task SplitTeam(int teamNumber = 2)
        {
            var members = ConnectedVoiceChannel().Users;
            var resultTeams = Split(members, teamNumber);

            var embed = EmbedBuilderFactory();

            var counter = 1;
            foreach (var team in resultTeams)
            {
                embed.AddField(
                    $"Team #{counter} ({GetVoiceChannelReletive(counter - 1)?.Name ?? ""})",
                    string.Join(Environment.NewLine, team.Select(user => user.Nickname ?? user.Username)) + Environment.NewLine
                    );
                counter++;
            }

            await ReplyEmbed(embed, $"`{members.Count}` 人のプレイヤーを `{teamNumber}` つのチームに分けました");

            Context.Client.RelationMemory.Memorise(
               Context.Channel,
               new SplitTeamConversation
               {
                   Teams = resultTeams,
                   TopChannel = GetVoiceChannelReletive(0)
               }
               );
        }

        [Command("assign"), Alias("a")]
        public async Task Assign()
        {
            var conv = Context.Client.RelationMemory.Recall(Context.Channel, new SplitTeamConversation().Topic);
            var stConv = (SplitTeamConversation)conv;

            var counter = 0;
            foreach (var team in stConv.Teams)
            {
                counter++;

                if (counter == 1)
                    continue;

                var destChannel = GetVoiceChannelReletive(counter - 1);
                if (destChannel == null)
                    break;

                foreach (var user in team)
                {
                    await user.ModifyAsync(u =>
                    {
                        u.Channel = destChannel;
                    });
                }
            }

            await ReplyAsync("```チームごとのチャンネルにプレイヤーを移動しました```");
        }

        [Command("recall"), Alias("r")]
        public async Task Recall()
        {
            var conv = Context.Client.RelationMemory.Recall(Context.Channel, new SplitTeamConversation().Topic);
            var stConv = (SplitTeamConversation)conv;

            for (var team = 1; team < stConv.Teams.Count(); team++)
            {
                var channel = GetVoiceChannelReletive(team);
                if (channel == null)
                    break;

                foreach (var user in channel.Users)
                {
                    await user.ModifyAsync(u =>
                    {
                        u.Channel = stConv.TopChannel;
                    });
                }
            }

            await ReplyAsync("```全プレイヤーを元のチャンネルに戻しました```");
        }

        private static IEnumerable<IEnumerable<T>> Split<T>(IEnumerable<T> sourceList, int teamNumber)
        {
            var list = sourceList.ToList();

            var rnd = new Random(); //Declaring this outside as a field will make this method thread-unsafe.
            var shuffledList = list.OrderBy(member => rnd.Next()).ToList(); //note: result will duplicate when called at the same time. 

            var memberPerTeam = (int)Math.Ceiling((double)list.Count / teamNumber); //because int / operator floors decimals.

            var result = shuffledList.SplitList(memberPerTeam);

            return result;
        }

        private SocketVoiceChannel GetVoiceChannelReletive(int offset)
        {
            if (offset == 0)
                return ConnectedVoiceChannel();

            var voicechannels = Context.Guild.VoiceChannels;

            return voicechannels.FirstOrDefault(c => c.Position == ConnectedVoiceChannel().Position + offset);
        }
        private class SplitTeamConversation : IConversation
        {
            public object Topic { get; set; } = nameof(SplitTeamCommand);
            public IEnumerable<IEnumerable<SocketGuildUser>> Teams { get; set; }
            public SocketVoiceChannel TopChannel { get; set; }
        }
    }
}
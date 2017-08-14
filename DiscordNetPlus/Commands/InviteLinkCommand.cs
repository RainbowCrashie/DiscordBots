using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordNetPlus.Commands
{
    [Group("invitelink"), Alias("il", "link"), RequireUserPermission(GuildPermission.CreateInstantInvite), RequireContext(ContextType.Guild)]
    public class InviteLinkComamand : GuildCommandBase
    {
        [Command("set"), RequireUserPermission(GuildPermission.Administrator)]
        public async Task Set()
        {
            //set
        }

        [Command("")]
        public async Task Dm()
        {
            await GrantUrl(await Context.User.CreateDMChannelAsync());
        }

        public async Task GrantUrl(IChannel replyChannel)
        {
            var a = Context.Channel as SocketGuildChannel;
            //get
        }
    }
}
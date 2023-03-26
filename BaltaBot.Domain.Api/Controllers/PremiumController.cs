using BaltaBot.Domain.Commands;
using BaltaBot.Domain.Handlers;
using Discord;
using Discord.Commands;
using Microsoft.AspNetCore.Mvc;

namespace BaltaBot.Domain.Api.Controllers
{
    public class PremiumController : ModuleBase<SocketCommandContext>
    {
        [Command("premium")]
        public async Task SetPremium([FromServices] PremiumHandler handler, string id)
        {
            if (Context.Message.Channel.Name != "quero-ser-premium")
                return;

            await Context.Channel.DeleteMessageAsync(Context.Message.Id);
            var command = new CreatePremiumCommand(id, Context.User.Id.ToString());

            var result = (GenericCommandResult)await handler.Handle(command);

            if (result.Success)
            {
                var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Premium");
                await (Context.User as IGuildUser).AddRoleAsync(role);
            }

            await ReplyAsync(result.Message);
        }
    }
}

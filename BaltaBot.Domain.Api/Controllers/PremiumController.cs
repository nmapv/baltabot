﻿using BaltaBot.Domain.Commands;
using BaltaBot.Domain.Handlers;
using Discord;
using Discord.Commands;

namespace BaltaBot.Domain.Api.Controllers
{
    public class PremiumController : ModuleBase<SocketCommandContext>
    {
        private readonly PremiumHandler _handler;

        public PremiumController(PremiumHandler handler) 
        {
            _handler = handler;
        }

        [Command("premium")]
        public async Task SetPremium(string id)
        {
            if (Context.Message.Channel.Name != "quero-ser-premium")
                return;

            await Context.Channel.DeleteMessageAsync(Context.Message.Id);
            var command = new CreatePremiumCommand(id, Context.User.Id.ToString());

            var result = (GenericCommandResult)await _handler.Handle(command);

            if (result.Success)
            {
                var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Premium");
                await (Context.User as IGuildUser).AddRoleAsync(role);
            }

            await ReplyAsync(result.Message);
        }

        [Command("cleaning")]
        public async Task Cleaning()
        {
            if (Context.Message.Channel.Name != "quero-ser-premium")
                return;

            await Context.Channel.DeleteMessageAsync(Context.Message.Id);
            var command = new GetPremiumInactiveCommand();

            var result = (GenericCommandResult)await _handler.Handle(command);

            if (result.Success)
            {
                var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Premium");
                var ids = (List<string>)result.Data;
                for (int i = 0; i < ids.Count(); i++)
                {
                    var id = ids[i];
                    if (string.IsNullOrEmpty(id))
                        continue;

                    var user = await Context.Channel.GetUserAsync(ulong.Parse(id));
                    await (user as IGuildUser).RemoveRoleAsync(role);
                    await _handler.Handle(new DeletePremiumCommand(id));
                }
            }

            await ReplyAsync(result.Message);
        }
    }
}

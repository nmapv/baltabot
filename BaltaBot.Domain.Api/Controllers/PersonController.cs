using BaltaBot.Domain.Commands;
using BaltaBot.Domain.Handlers;
using Discord;
using Discord.Commands;

namespace BaltaBot.Domain.Api.Controllers
{
    public class PersonController : ModuleBase<SocketCommandContext>
    {
        private readonly PersonHandler _handler;

        public PersonController(PersonHandler handler)
        {
            _handler = handler;
        }

        [Command("verify")]
        public async Task CreatePerson()
        {
            if (Context.Message.Channel.Name != "quero-ser-premium")
                return;

            await Context.Channel.DeleteMessageAsync(Context.Message.Id);
            var command = new CreatePersonCommand(Context.User.Id.ToString(), Context.User.Username);

            var result = (GenericCommandResult)await _handler.Handle(command);
            
            if (result.Success)
            {
                var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Verificado");
                await (Context.User as IGuildUser).AddRoleAsync(role);
            }

            await ReplyAsync(result.Message);
        }
    }
}

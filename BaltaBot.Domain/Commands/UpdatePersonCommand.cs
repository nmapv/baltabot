using BaltaBot.Domain.Commands.Interfaces;
using Flunt.Notifications;
using Flunt.Validations;

namespace BaltaBot.Domain.Commands
{
    public class UpdatePersonCommand : Notifiable<Notification>, ICommand
    {
        public UpdatePersonCommand() { }

        public UpdatePersonCommand(string name, string discordId)
        {
            Name = name;
            DiscordId = discordId;
        }

        public string Name { get; private set; }
        public string DiscordId { get; private set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<CreatePersonCommand>()
                    .Requires()
                    .IsNotNullOrEmpty(Name, "Name", "Nome é inválido")
                    .IsNotNullOrEmpty(DiscordId, "DiscordId", "Discord id é inválido")
            );
        }
    }
}

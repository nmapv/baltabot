using BaltaBot.Domain.Commands.Interfaces;
using Flunt.Notifications;
using Flunt.Validations;

namespace BaltaBot.Domain.Commands
{
    public class CreatePersonCommand : Notifiable<Notification>, ICommand
    {
        public CreatePersonCommand() { }

        public CreatePersonCommand(string discordId, string name)
        {
            DiscordId = discordId;
            Name = name;
            CreatedAt = DateTime.Now;
        }

        public string DiscordId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<CreatePersonCommand>()
                    .Requires()
                    .IsNotNullOrEmpty(DiscordId, "DiscordId", "Discord Id é inválido")
                    .IsNotNullOrEmpty(Name, "Name", "Nome é inválido")
            );
        }
    }
}

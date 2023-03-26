using BaltaBot.Domain.Commands.Interfaces;
using BaltaBot.Domain.Entities;
using Flunt.Notifications;
using Flunt.Validations;

namespace BaltaBot.Domain.Commands
{
    public class CreatePremiumCommand : Notifiable<Notification>, ICommand
    {
        public CreatePremiumCommand() { }

        public CreatePremiumCommand(string id, string discordId)
        {
            Id = id;
            DiscordId = discordId;
        }

        public string Id { get; private set; }
        public string DiscordId { get; private set; }

        public Guid GetGuid()
        {
            return Guid.Parse(Id);
        }

        public void Validate()
        {
            AddNotifications(
                new Contract<Premium>()
                    .Requires()
                    .IsFalse(Guid.TryParse(Id, out _), "Id", "Id inválido")
                    .IsNotNull(DiscordId, "DiscordId", "Discord id inválido")
            );
        }
    }
}

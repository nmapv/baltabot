using BaltaBot.Domain.Commands.Interfaces;
using BaltaBot.Domain.Entities;
using Flunt.Notifications;
using Flunt.Validations;

namespace BaltaBot.Domain.Commands
{
    public class DeletePremiumCommand : Notifiable<Notification>, ICommand
    {
        public DeletePremiumCommand(string discordId)
        {
            DiscordId = discordId;
        }

        public string DiscordId { get; private set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Premium>()
                    .Requires()
                    .IsNotNull(DiscordId, "DiscordId", "Discord id inválido")
            );
        }
    }
}

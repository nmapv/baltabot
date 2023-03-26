using BaltaBot.Domain.Commands.Interfaces;
using BaltaBot.Domain.Entities;
using Flunt.Notifications;
using Flunt.Validations;

namespace BaltaBot.Domain.Commands
{
    public class CreatePremiumCommand : Notifiable<Notification>, ICommand
    {
        public CreatePremiumCommand() { }

        public CreatePremiumCommand(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }

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
            );
        }
    }
}

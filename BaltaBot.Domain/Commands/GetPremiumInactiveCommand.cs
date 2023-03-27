using BaltaBot.Domain.Commands.Interfaces;
using Flunt.Notifications;

namespace BaltaBot.Domain.Commands
{
    public class GetPremiumInactiveCommand : Notifiable<Notification>, ICommand
    {
        public void Validate()
        {
        }
    }
}

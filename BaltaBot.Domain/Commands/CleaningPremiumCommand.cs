using BaltaBot.Domain.Commands.Interfaces;
using Flunt.Notifications;

namespace BaltaBot.Domain.Commands
{
    public class CleaningPremiumCommand : Notifiable<Notification>, ICommand
    {
        public void Validate()
        {
        }
    }
}

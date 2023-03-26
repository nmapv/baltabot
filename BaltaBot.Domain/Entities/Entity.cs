using Flunt.Notifications;

namespace BaltaBot.Domain.Entities
{
    public abstract class Entity : Notifiable<Notification>
    {
        public Entity()
        {
            Id = Guid.NewGuid(); 
        }

        public Guid Id { get; set; }
    }
}

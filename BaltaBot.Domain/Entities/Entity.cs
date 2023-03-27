using Dapper.Contrib.Extensions;

namespace BaltaBot.Domain.Entities
{
    public abstract class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        [ExplicitKey]
        public Guid Id { get; set; }
    }
}

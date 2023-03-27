using Dapper.Contrib.Extensions;

namespace BaltaBot.Domain.Entities
{
    [Table("Person")]
    public class Person : Entity
    {
        public Person(string discordId, string name, DateTime createdAt)
        {
            DiscordId = discordId;
            Name = name;
            CreatedAt = createdAt;
        }

        public Person(string Id, string DiscordId, string Name, string CreatedAt)
        {
            this.Id = Guid.Parse(Id);
            this.DiscordId = DiscordId;
            this.Name = Name;
            this.CreatedAt = DateTime.Parse(CreatedAt);
        }

        public string DiscordId { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public void SetName(string name)
        {
            Name = name;
        }
    }
}

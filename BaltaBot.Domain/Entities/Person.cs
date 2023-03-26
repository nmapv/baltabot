using BaltaBot.Domain.Commands;
using Dapper.Contrib.Extensions;
using Flunt.Validations;

namespace BaltaBot.Domain.Entities
{
    [Table("Person")]
    public class Person : Entity
    {
        public Person(string discordId, string name, DateTime createdAt)
        {
            AddNotifications(
               new Contract<CreatePersonCommand>()
                   .Requires()
                   .IsNotNullOrEmpty(discordId, "DiscordId", "Discord Id é inválido")
                   .IsNotNullOrEmpty(name, "Name", "Nome é inválido")
                   .IsNotNull(createdAt, "CreatedAt", "Data de criação é inválido")
            );

            DiscordId = discordId;
            Name = name;
            CreatedAt = createdAt;
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

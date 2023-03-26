using Dapper.Contrib.Extensions;
using Flunt.Validations;

namespace BaltaBot.Domain.Entities
{
    public class Premium : Entity
    {
        public Premium(Person person, string id, DateTime startedAt, DateTime closedAt)
        {
            AddNotifications(
                new Contract<Premium>()
                    .Requires()                    
                    .IsNotNull(id, "Id", "Id inválido")
                    .IsNotNull(person, "Person", "Pessoa inválida")
                    .IsNotNull(startedAt, "StartedAt", "Data de inínio inválido")
                    .IsNotNull(closedAt, "ClosedAt", "Data de término inválido")
                    .IsTrue(Expired(closedAt), "ClosedAt", "Data de término já expirada")
            );

            Id = Guid.Parse(id);
            Person = person;
            Person.Id = person.Id;
            StardetAt = startedAt;
            ClosedAt = closedAt;
        }

        [Write(false)]
        public Person Person { get; private set; }
        public DateTime StardetAt { get; private set; }
        public DateTime ClosedAt { get; private set; }
        public Guid PersonId { get; private set; }

        public bool Expired(DateTime? closedAt = null)
        {
            return DateTime.Compare(DateTime.Now, closedAt ?? ClosedAt) >= 0;
        }
    }
}

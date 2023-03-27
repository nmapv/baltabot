using Dapper.Contrib.Extensions;

namespace BaltaBot.Domain.Entities
{
    [Table("Premium")]
    public class Premium : Entity
    {
        public Premium(string id, DateTime startedAt, DateTime closedAt, Person person)
        {
            Id = Guid.Parse(id);
            StartedAt = startedAt;
            ClosedAt = closedAt;
            Person = person;
            PersonId = person.Id;
        }

        public Premium(string Id, string PersonId, string StartedAt, string ClosedAt)
        {
            this.Id = Guid.Parse(Id);
            this.PersonId = Guid.Parse(PersonId);
            this.StartedAt = DateTime.Parse(StartedAt);
            this.ClosedAt = DateTime.Parse(ClosedAt);
        }

        [Write(false)]
        public Person Person { get; private set; }
        public DateTime StartedAt { get; private set; }
        public DateTime ClosedAt { get; private set; }
        public Guid PersonId { get; private set; }

        public bool Expired(DateTime? closedAt = null)
        {
            return DateTime.Compare(DateTime.Now, closedAt ?? ClosedAt) >= 0;
        }
    }
}

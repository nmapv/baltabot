using BaltaBot.Domain.Entities;

namespace BaltaBot.Domain.Repositories
{
    public interface IPremiumRepository : IRepository<Premium>
    {
        Task GetByPerson(Person person);
    }
}

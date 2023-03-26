using BaltaBot.Domain.Entities;

namespace BaltaBot.Domain.Repositories
{
    public interface IPremiumApiRepository
    {
        Task<Premium> Create(Guid id, Person person);
    }
}

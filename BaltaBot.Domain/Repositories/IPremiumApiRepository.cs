using BaltaBot.Domain.Entities;

namespace BaltaBot.Domain.Repositories
{
    public interface IPremiumApiRepository
    {
        Task<Premium> GetById(Guid id);
    }
}

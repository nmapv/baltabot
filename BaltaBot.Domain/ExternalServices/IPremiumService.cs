using BaltaBot.Domain.Entities;

namespace BaltaBot.Domain.ExternalServices
{
    public interface IPremiumService
    {
        Task<Premium?> Get(Guid id, Person person);
    }
}

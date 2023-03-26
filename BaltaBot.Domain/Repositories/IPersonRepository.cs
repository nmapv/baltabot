using BaltaBot.Domain.Entities;

namespace BaltaBot.Domain.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<Person> GetByDiscordId(string  discordId);
    }
}

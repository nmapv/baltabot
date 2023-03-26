using BaltaBot.Domain.Entities;
using BaltaBot.Domain.Infra.Context;
using BaltaBot.Domain.Repositories;
using DapperExtensions;
using DapperExtensions.Predicate;

namespace BaltaBot.Domain.Infra.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<Person?> GetByDiscordId(string discordId)
        {
            var predicate = Predicates.Field<Person>(f => f.DiscordId, Operator.Eq, discordId);
            var result = await DataContext.connection.GetListAsync<Person>(predicate);

            return result.FirstOrDefault();
        }
    }
}

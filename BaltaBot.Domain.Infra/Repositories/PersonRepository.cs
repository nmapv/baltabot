using BaltaBot.Domain.Entities;
using BaltaBot.Domain.Infra.Context;
using BaltaBot.Domain.Repositories;
using Dapper;

namespace BaltaBot.Domain.Infra.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<Person?> GetByDiscordId(string discordId)
        {
            var result = await DataContext.connection.QueryAsync<Person>(@"select * from Person where DiscordId=@discordId", param: new { discordId });
            return result.FirstOrDefault();
        }
    }
}

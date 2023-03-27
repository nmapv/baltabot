using BaltaBot.Domain.Entities;
using BaltaBot.Domain.Infra.Context;
using BaltaBot.Domain.Repositories;
using Dapper;

namespace BaltaBot.Domain.Infra.Repositories
{
    public class PremiumRepository : Repository<Premium>, IPremiumRepository
    {
        public PremiumRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<IEnumerable<Premium>> GetInactives()
        {
            var result = await DataContext.connection.QueryAsync<Premium>(@"select * from Premium where ClosedAt>=GETDATE()");
            return result;
        }
    }
}

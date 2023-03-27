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
            var premiumDictionary = new Dictionary<Guid, Premium>();
            var result = await DataContext.connection.QueryAsync<Premium, Person, Premium>(@"select * from Premium pr, Person pe where pr.Person_id = pe.Id and pr.ClosedAt>=GETDATE()",
                (premium, person) =>
                {
                    Premium premiumEntry;

                    if (!premiumDictionary.TryGetValue(premium.Id, out premiumEntry))
                    {
                        premiumEntry = premium;
                        premiumDictionary.Add(premiumEntry.Id, premiumEntry);
                    }

                    premiumEntry.SetPerson(person);
                    return premiumEntry;
                },
                splitOn: "Id"
            );
            return result;
        }

        public async Task DeleteInactives()
        {
            await DataContext.connection.QueryAsync(@"delete Premium where ClosedAt>=GETDATE()");
        }
    }
}

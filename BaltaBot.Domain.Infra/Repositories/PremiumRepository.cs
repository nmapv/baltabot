using BaltaBot.Domain.Entities;
using BaltaBot.Domain.Infra.Context;
using BaltaBot.Domain.Repositories;

namespace BaltaBot.Domain.Infra.Repositories
{
    public class PremiumRepository : Repository<Premium>, IPremiumRepository
    {
        public PremiumRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}

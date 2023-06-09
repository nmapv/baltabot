﻿using BaltaBot.Domain.Entities;
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
            var today = DateTime.Now;
            var result = await DataContext.connection.QueryAsync<Premium, Person, Premium>(@"select * from Premium pr, Person pe where pr.PersonId = pe.Id and pr.ClosedAt<=@today",
                (premium, person) =>
                {
                    premium.SetPerson(person);
                    return premium;
                },
                splitOn: "Id",
                param: new { today }
            );
            return result;
        }

        public async Task DeleteByDiscorId(string discordId)
        {
            await DataContext.connection.QueryAsync(@"delete pr from Premium pr join Person pe on pr.PersonId = pe.Id where pe.DiscordId=@discordId", param: new { discordId });
        }
    }
}

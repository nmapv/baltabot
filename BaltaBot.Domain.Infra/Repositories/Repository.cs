using BaltaBot.Domain.Entities;
using BaltaBot.Domain.Infra.Context;
using BaltaBot.Domain.Repositories;
using Dapper.Contrib.Extensions;

namespace BaltaBot.Domain.Infra.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        protected DataContext DataContext;

        public Repository(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public async Task Create(T entity)
        {
            await DataContext.connection.InsertAsync(entity);
        }

        public async Task<T> Get(Guid id)
        {
            return await DataContext.connection.GetAsync<T>(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await DataContext.connection.GetAllAsync<T>();
        }

        public async Task Update(T entity)
        {
            await DataContext.connection.UpdateAsync(entity);
        }
    }
}

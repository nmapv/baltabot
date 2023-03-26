using BaltaBot.Domain.Entities;

namespace BaltaBot.Domain.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task Create(T entity);
        Task Update(T entity);
        Task<T> Get(Guid id);
        Task<IEnumerable<T>> GetAll();        
    }
}

using Mefo.Orp.Backend.Models.Entities.Abstracts;

namespace Mefo.Orp.Backend.Data.Repositories.Abstracts
{
    public interface IRepository<T> where T : IBaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
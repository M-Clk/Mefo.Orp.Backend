using Mefo.Orp.Backend.Data.Repositories.Abstracts;
using Mefo.Orp.Backend.Models.Entities.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Mefo.Orp.Backend.Data.Repositories;

public class Repository<T>(DbContext dbContext) : IRepository<T> where T : class, IBaseEntity
{
    private readonly DbSet<T> _dbSet = dbContext.Set<T>();

    public virtual async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public virtual async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public virtual async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public virtual void Update(T entity) => _dbSet.Update(entity);

    public virtual void Delete(T entity) => _dbSet.Remove(entity);
}
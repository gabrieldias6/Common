using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GD6.Common
{
    public class Repository<TEntity> : IRepository<TEntity> 
        where TEntity : class, IEntity, new()
    {
        public readonly DbContext _context;

        public Repository(DbContext dbContext)
        {
            _context = dbContext;
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await _context.Set<TEntity>()
                      .AsNoTracking()
                      .FirstOrDefaultAsync(e => e.Id == id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context
              .Set<TEntity>()
              .AsNoTracking();
        }

        public virtual async Task Create(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task CreateMany(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Update(int id, TEntity entity)
        {
            entity.Id = id;
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateMany(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteMany(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GD6.Common
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : class, IEntityBase, new()
    {
        public readonly IUnitOfWork UnitOfWork;

        public RepositoryBase(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await UnitOfWork.Current.Set<TEntity>()
                      .AsNoTracking()
                      .FirstOrDefaultAsync(e => e.Id == id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return UnitOfWork.Current
              .Set<TEntity>()
              .AsNoTracking();
        }

        public virtual async Task Create(TEntity entity)
        {
            await UnitOfWork.Current.Set<TEntity>().AddAsync(entity);
            await UnitOfWork.Current.SaveChangesAsync();
        }

        public virtual async Task CreateMany(IEnumerable<TEntity> entities)
        {
            await UnitOfWork.Current.Set<TEntity>().AddRangeAsync(entities);
            await UnitOfWork.Current.SaveChangesAsync();
        }

        public virtual async Task Update(int id, TEntity entity)
        {
            entity.Id = id;
            UnitOfWork.Current.Set<TEntity>().Update(entity);
            await UnitOfWork.Current.SaveChangesAsync();  
        }

        public virtual async Task UpdateMany(IEnumerable<TEntity> entities)
        {
            UnitOfWork.Current.Set<TEntity>().UpdateRange(entities);
            await UnitOfWork.Current.SaveChangesAsync();
        }

        public virtual async Task Delete(TEntity entity)
        {
            UnitOfWork.Current.Set<TEntity>().Remove(entity);
            await UnitOfWork.Current.SaveChangesAsync();
        }

        public virtual async Task DeleteMany(IEnumerable<TEntity> entities)
        {
            UnitOfWork.Current.Set<TEntity>().RemoveRange(entities);
            await UnitOfWork.Current.SaveChangesAsync();
        }
    }
}

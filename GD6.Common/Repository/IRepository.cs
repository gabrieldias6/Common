using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GD6.Common
{
    public interface IRepository<TEntity> 
        where TEntity : class, IEntity
    {
        Task<TEntity> GetById(int id);

        IQueryable<TEntity> GetAll();

        Task Create(TEntity entity);

        Task CreateMany(IEnumerable<TEntity> entities);

        Task Update(int id, TEntity entity);

        Task UpdateMany(IEnumerable<TEntity> entities);

        Task Delete(TEntity entity);

        Task DeleteMany(IEnumerable<TEntity> entities);
    }
}

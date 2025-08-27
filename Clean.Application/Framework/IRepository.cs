using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Clean.Application.Framework
{
    public interface IRepository<TEntity, in TKey>
                                        where TEntity : class
                                        where TKey : struct
    {

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TKey id);
        Task DeleteAsync(TKey id);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task<bool> Exists(Expression<Func<TEntity, bool>> expression);
        Task SaveChangesAsync();
        IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableGlobalFilter = false, bool disableTracking = true, bool disableMaxRowLimit = false);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableGlobalFilter = false, bool disableTracking = true);

    }
}

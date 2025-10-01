using Clean.Application.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Clean.Repository.Framework
{
    public class BaseRepository<K_DbContext, TEntity, TKey> : IRepository<TEntity, TKey>
                                                    where TEntity : class
                                                    where TKey : struct
                                                    where K_DbContext : DbContext
    {
        public virtual K_DbContext Context { get; set; }
        public virtual DbSet<TEntity> Db_Set { get; set; }

        public BaseRepository(K_DbContext context)
        {
            Context = context;
            Db_Set = context.Set<TEntity>();
        }

        public virtual async Task CreateAsync(TEntity entity)
        {
            await Db_Set.AddAsync(entity);
        }

        public virtual async Task DeleteAsync(TKey id)
        {
            var query = await Db_Set.FindAsync(id);
            Db_Set.Remove(query);
            await SaveChangesAsync();
        }

        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> expression)
        {
            var query = await Context.Set<TEntity>().AnyAsync(expression);
            return query;
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableGlobalFilter = false, bool disableTracking = true)
        {
            return await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(GetQueryable(predicate, orderBy, include, disableGlobalFilter, disableTracking));
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var query = await Db_Set.AsNoTracking().ToListAsync();
            return query;
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            var query = await Db_Set.FindAsync(id);
            return query;
        }

        public virtual IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableGlobalFilter = false, bool disableTracking = true, bool disableMaxRowLimit = false)
        {
            IQueryable<TEntity> queryable = EntityFrameworkQueryableExtensions.TagWith(Db_Set.AsQueryable(), "Repository GetQueryable");
            if (disableGlobalFilter)
            {
                queryable = EntityFrameworkQueryableExtensions.IgnoreQueryFilters(queryable);
            }

            if (disableTracking)
            {
                queryable = EntityFrameworkQueryableExtensions.AsNoTracking(queryable);
            }

            if (include != null)
            {
                queryable = include(queryable);
            }

            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }

            if (orderBy != null)
            {
                queryable = orderBy(queryable);
            }

            if (!disableMaxRowLimit)
            {
                queryable = queryable.Take(1000);
            }

            return queryable;
        }

        public virtual async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            Db_Set.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            await SaveChangesAsync();
        }
    }
}

using System.Linq.Expressions;
using ForumTask.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ForumTask.DAL.EF
{
    /// <summary>
    /// Default realization of <see cref="IRepository{T}"/>
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    internal class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> set;

        public GenericRepository(DbContext context)
        {
            set = context.Set<T>();
        }

        public Task CreateAsync(T entity)
        {
            set.Add(entity);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            set.Remove(entity);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderFunc = null,
            int skipCount = 0,
            int takeCount = 0)
        {
            var queryable = GetConfiguredQueryable(predicate, orderFunc, skipCount, takeCount);

            return queryable.ToListAsync().ContinueWith(x => (IEnumerable<T>)x);
        }

        public Task<long> CountAsync(
            Expression<Func<T, bool>> predicate = null,
            int skipCount = 0,
            int takeCount = 0)
        {
            var queryable = GetConfiguredQueryable(predicate, orderFunc: null, skipCount, takeCount);

            return queryable.LongCountAsync();
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return set.FirstOrDefaultAsync(predicate);
        }

        public Task UpdateAsync(T entity)
        {
            set.Update(entity);

            return Task.CompletedTask;
        }

        private IQueryable<T> GetConfiguredQueryable(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderFunc = null,
            int skipCount = 0,
            int takeCount = 0)
        {
            IQueryable<T> result = set;

            if (predicate != null)
            {
                result = result.Where(predicate);
            }
            if (orderFunc != null)
            {
                result = orderFunc(result);
            }
            if (skipCount > 0)
            {
                result = result.Skip(skipCount);
            }
            if (takeCount > 0)
            {
                result = result.Take(takeCount);
            }

            return result;
        }
    }
}

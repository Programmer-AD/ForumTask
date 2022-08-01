using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumTask.DAL.Interfaces
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Creates/adds/inserts entity
        /// </summary>
        Task CreateAsync(T entity);

        /// <summary>
        /// Finds entity by providden predicate
        /// </summary>
        /// <returns>Found entity or null if not found</returns>
        Task<T> GetAsync(Func<T, bool> predicate);

        /// <summary>
        /// Finds all entities by predicate with ordering, offset and take limit
        /// </summary>
        /// <param name="predicate">Predicate to use</param>
        /// <param name="orderBy">Ordering of result</param>
        /// <param name="offset">Count to skip (if <=0 then not used)</param>
        /// <param name="take">Count to take (if <=0 then not used)</param>
        /// <returns>Collection of entities</returns>
        Task<IEnumerable<T>> GetAllAsync(
            Func<T, bool> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int offset = 0,
            int take = 0);

        /// <summary>
        /// Finds all entities by predicate with ordering, offset and take limit
        /// </summary>
        /// <param name="predicate">Predicate to use</param>
        /// <param name="offset">Count to skip (if <=0 then not used)</param>
        /// <param name="take">Maximum count (if <=0 then not used)</param>
        /// <returns>Count of matching entities</returns>
        Task<long> CountAsync(
            Func<T, bool> predicate = null,
            int offset = 0,
            int take = 0);

        /// <summary>
        /// Updates entity
        /// </summary>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Deletes entity
        /// </summary>
        Task DeleteAsync(T entity);
    }
}

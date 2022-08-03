using System.Linq.Expressions;

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
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Finds all entities by predicate with ordering, offset and take limit
        /// </summary>
        /// <param name="predicate">Predicate to use</param>
        /// <param name="orderFunc">Ordering of result</param>
        /// <param name="skipCount">Count to skip (if <=0 then not used)</param>
        /// <param name="takeCount">Count to take (if <=0 then not used)</param>
        /// <returns>Collection of entities</returns>
        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderFunc = null,
            int skipCount = 0,
            int takeCount = 0);

        /// <summary>
        /// Finds all entities by predicate with ordering, offset and take limit
        /// </summary>
        /// <param name="predicate">Predicate to use</param>
        /// <param name="skipCount">Count to skip (if <=0 then not used)</param>
        /// <param name="takeCount">Maximum count (if <=0 then not used)</param>
        /// <returns>Count of matching entities</returns>
        Task<long> CountAsync(
            Expression<Func<T, bool>> predicate = null,
            int skipCount = 0,
            int takeCount = 0);
        Task ForceSaveChangesAsync();

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

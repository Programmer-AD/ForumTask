using System;

namespace ForumTask.DAL.Interfaces {
    public interface IRepository<T> {
        /// <summary>
        /// Creates/adds/inserts entity
        /// <para>
        /// If <paramref name="ent"/> is null, throws <see cref="ArgumentNullException"/> 
        /// </para>
        /// </summary>
        /// <param name="ent">Entity to add</param>
        /// <exception cref="ArgumentNullException"></exception>
        void Create(T ent);

        /// <summary>
        /// Finds entity with providden primary key
        /// <para>
        /// If <paramref name="key"/> is null, throws <see cref="ArgumentNullException"/> 
        /// </para>
        /// </summary>
        /// <param name="key">Primary key</param>
        /// <returns>Found entity or null if not found</returns>
        /// <exception cref="ArgumentNullException" />
        T Get(params object[] key);

        /// <summary>
        /// Updates entity
        /// <para>
        /// If <paramref name="ent"/> is null, throws <see cref="ArgumentNullException"/> 
        /// </para>
        /// </summary>
        /// <param name="ent">Entity to update</param>
        /// <exception cref="ArgumentNullException" />
        void Update(T ent);

        /// <summary>
        /// Deletes entity with providden primary key
        /// <para>
        /// If <paramref name="key"/> is null, throw <see cref="ArgumentNullException"/>
        /// </para>
        /// <para>
        /// If entity with such keys isn`t found, throws <see cref="InvalidOperationException"/>
        /// </para>
        /// </summary>
        /// <param name="key">Primary key</param>
        /// <exception cref="InvalidOperationException">/>
        /// <exception cref="ArgumentNullException"/>
        void Delete(params object[] key);

        /// <summary>
        /// Deletes entity
        /// <para>
        /// If <paramref name="ent"/> is null, throws <see cref="ArgumentNullException"/> 
        /// </para>
        /// </summary>
        /// <param name="ent">Entity to delete</param>
        /// <exception cref="ArgumentNullException"/>
        void Delete(T ent);
    }
}

using ForumTask.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace ForumTask.DAL.EF.Repositories {
    /// <summary>
    /// Default realization of <see cref="IRepository{T}"/>
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    class GenericRepository<T> : IRepository<T> where T : class {
        protected readonly ForumContext db;
        protected readonly DbSet<T> set;

        protected GenericRepository(ForumContext context) {
            db = context;
            set = context.Set<T>();
        }

        public void Create(T ent)
            => set.Add(ent ?? throw new ArgumentNullException(nameof(ent)));

        public void Delete(params object[] key) {
            T t = Get(key ?? throw new ArgumentNullException(nameof(key)));
            Delete(t ?? throw new InvalidOperationException("Object with such keys wasn`t found, so can`t be deleted"));
        }

        public void Delete(T ent)
            => set.Remove(ent ?? throw new ArgumentNullException(nameof(ent)));

        public T Get(params object[] key)
            => set.Find(key ?? throw new ArgumentNullException(nameof(key)));

        public void Update(T ent)
            => set.Update(ent ?? throw new ArgumentNullException(nameof(ent)));
    }
}

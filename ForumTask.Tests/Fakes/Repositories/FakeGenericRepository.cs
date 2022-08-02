using System;
using System.Collections.Generic;
using System.Linq;
using ForumTask.DAL.Interfaces;

namespace ForumTask.Tests.Fakes.Repositories
{
    internal abstract class FakeGenericRepository<T> : IRepository<T>
    {
        protected readonly List<T> data;

        public FakeGenericRepository(params T[] data)
        {
            this.data = data.ToList();
        }

        /// <summary>
        /// Checks if <paramref name="key"/> is correct key for this object
        /// </summary>
        /// <param name="key">Key to check (not null)</param>
        /// <returns>Is key correct?</returns>
        protected abstract bool IsKeyCorrect(object[] key);
        /// <summary>
        /// Checks if <paramref name="key"/> is key of <paramref name="ent"/>
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <param name="ent">Entity which to check</param>
        /// <returns>Is this key of this entity?</returns>
        protected abstract bool IsKeyOfObjectPredicate(object[] key, T ent);
        /// <summary>
        /// Checks if entities has same keys
        /// </summary>
        /// <param name="ent">Entity 1</param>
        /// <param name="ent2">Entity 2</param>
        /// <returns>Entity has same keys?</returns>
        protected abstract bool IsSameObjectKeyPredicate(T ent, T ent2);
        /// <summary>
        /// Assigns new key to entity
        /// </summary>
        /// <param name="ent">Entity to which key must be setted</param>
        protected abstract void AssignKey(T ent);

        protected void CheckKey(object[] key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (!IsKeyCorrect(key))
            {
                throw new ArgumentException("Key is wrong");
            }
        }

        protected T Find(T ent)
        {
            if (ent is null)
            {
                throw new ArgumentNullException(nameof(ent));
            }

            return data.Find(ent2 => IsSameObjectKeyPredicate(ent, ent2));
        }
        public bool Contains(T ent)
        {
            return Find(ent) is not null;
        }

        public void Create(T ent)
        {
            if (Contains(ent))
            {
                throw new InvalidOperationException("Object already in list");
            }

            AssignKey(ent);
            data.Add(ent);
        }

        public void Delete(params object[] key)
        {
            DeleteAsync(GetAsync(key));
        }

        public void DeleteAsync(T ent)
        {
            if (ent is null || !Contains(ent))
            {
                throw new InvalidOperationException("No such object in list");
            }

            data.Remove(ent);
        }

        public T GetAsync(params object[] key)
        {
            CheckKey(key);
            return data.Find(ent => IsKeyOfObjectPredicate(key, ent));
        }

        public void Update(T ent)
        {
            var el = Find(ent);
            if (el is null)
            {
                throw new InvalidOperationException("No such object in list");
            }

            data.Remove(el);
            data.Add(ent);
        }
    }
}

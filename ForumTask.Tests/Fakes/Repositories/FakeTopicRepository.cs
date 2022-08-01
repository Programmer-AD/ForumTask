﻿using System.Collections.Generic;
using System.Linq;
using ForumTask.DAL.Entities;
using ForumTask.DAL.Interfaces;

namespace ForumTask.Tests.Fakes.Repositories
{
    internal class FakeTopicRepository : FakeGenericRepository<Topic>, ITopicRepository
    {
        public FakeTopicRepository(params Topic[] data) : base(data) { }

        public int Count()
        {
            return data.Count;
        }

        public IEnumerable<Topic> GetTopNew(int count, int offset, string searchTitle = "")
        {
            return data.Where(t => t.Title.ToLower().Contains(searchTitle.ToLower())).Skip(offset).Take(count);
        }

        protected override void AssignKey(Topic ent)
        {
            ent.Id = data.Count == 0 ? 1 : data.Max(t => t.Id) + 1;
        }

        protected override bool IsKeyCorrect(object[] key)
        {
            return key.Length == 1 && key[0] is long;
        }

        protected override bool IsKeyOfObjectPredicate(object[] key, Topic ent)
        {
            return ent.Id == (long)key[0];
        }

        protected override bool IsSameObjectKeyPredicate(Topic ent, Topic ent2)
        {
            return ent.Id == ent2.Id;
        }
    }
}

using System;
using ForumTask.DAL.Entities;
using ForumTask.DAL.Interfaces;

namespace ForumTask.Tests.Fakes.Repositories
{
    internal class FakeMarkRepository : FakeGenericRepository<Mark>, IMarkRepository
    {
        public FakeMarkRepository(params Mark[] data) : base(data) { }

        public long GetCountOfType(long messageId, sbyte type)
        {
            throw new NotImplementedException();
        }

        protected override void AssignKey(Mark ent) { }

        protected override bool IsKeyCorrect(object[] key)
        {
            return key.Length == 2 && key[0] is int && key[1] is long;
        }

        protected override bool IsKeyOfObjectPredicate(object[] key, Mark ent)
        {
            return ent.UserId == (int)key[0] && ent.MessageId == (long)key[1];
        }

        protected override bool IsSameObjectKeyPredicate(Mark ent, Mark ent2)
        {
            return ent.UserId == ent2.UserId && ent.MessageId == ent2.MessageId;
        }
    }
}

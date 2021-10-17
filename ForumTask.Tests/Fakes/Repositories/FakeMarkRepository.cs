using System.Linq;
using ForumTask.DAL.Entities;
using ForumTask.DAL.Interfaces;

namespace ForumTask.Tests.Fakes.Repositories {
    class FakeMarkRepository : FakeGenericRepository<Mark>, IMarkRepository {
        public FakeMarkRepository(params Mark[] data) : base(data) { }

        public long GetMarkValue(long messageId)
            => data.Where(m => m.MessageId == messageId).Sum(m => (long)m.Type);

        protected override void AssignKey(Mark ent) { }

        protected override bool IsKeyCorrect(object[] key)
            => key.Length == 2 && key[0] is int && key[1] is long;

        protected override bool IsKeyOfObjectPredicate(object[] key, Mark ent)
            => ent.UserId == (int)key[0] && ent.MessageId == (long)key[1];

        protected override bool IsSameObjectKeyPredicate(Mark ent, Mark ent2)
            => ent.UserId == ent2.UserId && ent.MessageId == ent2.MessageId;
    }
}

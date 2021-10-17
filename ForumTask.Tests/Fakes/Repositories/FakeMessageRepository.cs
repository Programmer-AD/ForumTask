using System.Collections.Generic;
using System.Linq;
using ForumTask.DAL.Entities;
using ForumTask.DAL.Interfaces;

namespace ForumTask.Tests.Fakes.Repositories {
    class FakeMessageRepository : FakeGenericRepository<Message>, IMessageRepository {
        public FakeMessageRepository(params Message[] data) : base(data) { }

        public int GetMessageCount(long topicId)
            => data.Count(m => m.TopicId == topicId);

        public IEnumerable<Message> GetTopOld(long topicId, int count, int offset)
            => data.Where(m => m.TopicId == topicId).Skip(offset).Take(count);

        protected override void AssignKey(Message ent) {
            ent.Id = data.Count == 0 ? 1 : data.Max(m => m.Id) + 1;
        }

        protected override bool IsKeyCorrect(object[] key)
            => key.Length == 1 && key[0] is long;

        protected override bool IsKeyOfObjectPredicate(object[] key, Message ent)
            => ent.Id == (long)key[0];

        protected override bool IsSameObjectKeyPredicate(Message ent, Message ent2)
            => ent.Id == ent2.Id;
    }
}

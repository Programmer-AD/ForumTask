using System.Collections.Generic;
using System.Linq;
using ForumTask.DAL.Entities;
using ForumTask.DAL.Interfaces;

namespace ForumTask.DAL.EF.Repositories
{
    internal class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(ForumContext context) : base(context) { }

        public IEnumerable<Message> GetTopOld(long topicId, int count, int offset)
        {
            return set.Where(m => m.TopicId == topicId).OrderBy(m => m.WriteTime).Skip(offset).Take(count);
        }

        public int GetMessageCount(long topicId)
        {
            return set.Count(m => m.TopicId == topicId);
        }
    }
}

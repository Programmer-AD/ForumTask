using ForumTask.DAL.Entities;
using ForumTask.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumTask.DAL.EF.Repositories {
    class MessageRepository : GenericRepository<Message>, IMessageRepository {
        public MessageRepository(ForumContext context):base(context) { }

        public IEnumerable<Message> GetTopOld(Topic topic, int count, int offset) {
            if (topic is null)
                throw new ArgumentNullException(nameof(topic));
            return set.Where(m => m.TopicId == topic.Id).OrderBy(m => m.WriteTime).Skip(offset).Take(count);
        }
    }
}

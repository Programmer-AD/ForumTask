using ForumTask.DAL.Entities;
using ForumTask.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumTask.DAL.EF.Repositories {
    class TopicRepository : GenericRepository<Topic>, ITopicRepository {
        public TopicRepository(ForumContext context) : base(context) { }

        public IEnumerable<Topic> FindNewestByTitle(string title, int count, int offset)
            => set.Where(t => t.Title.Contains(title)).OrderByDescending(t => t.CreateTime).Skip(offset).Take(count);

        public int GetMessageCount(Topic topic) {
            if (topic is null) 
                throw new ArgumentNullException(nameof(topic));
            return db.Messages.Where(m => m.TopicId == topic.Id).Count();
        }

        public IEnumerable<Topic> GetTopNew(int count, int offset)
            => set.OrderByDescending(t => t.CreateTime).Skip(offset).Take(count);
    }
}

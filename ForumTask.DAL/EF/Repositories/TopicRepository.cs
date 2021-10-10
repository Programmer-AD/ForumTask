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

        public int Count()
            => set.Count();

        public IEnumerable<Topic> GetTopNew(int count, int offset, string searchTitle="") {
            IQueryable<Topic> col = set;
            if (!string.IsNullOrEmpty(searchTitle))
                col = col.Where(t => t.Title.Contains(searchTitle, StringComparison.OrdinalIgnoreCase));
            return col.OrderByDescending(t => t.CreateTime).Skip(offset).Take(count);
        }
    }
}

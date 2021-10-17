using System.Collections.Generic;
using System.Linq;
using ForumTask.DAL.Entities;
using ForumTask.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Ef = Microsoft.EntityFrameworkCore.EF;

namespace ForumTask.DAL.EF.Repositories {
    class TopicRepository : GenericRepository<Topic>, ITopicRepository {
        public TopicRepository(ForumContext context) : base(context) { }

        public int Count()
            => set.Count();

        public IEnumerable<Topic> GetTopNew(int count, int offset, string searchTitle = "") {
            IQueryable<Topic> col = set;
            if (!string.IsNullOrEmpty(searchTitle)) {
                searchTitle = $"%{searchTitle}%";
                col = col.Where(t => Ef.Functions.Like(t.Title, searchTitle));
            }
            return col.OrderByDescending(t => t.CreateTime).Skip(offset).Take(count);
        }
    }
}

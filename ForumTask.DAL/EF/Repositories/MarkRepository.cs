using ForumTask.DAL.Entities;
using ForumTask.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumTask.DAL.EF.Repositories {
    class MarkRepository : GenericRepository<Mark>, IMarkRepository {
        public MarkRepository(ForumContext context):base(context) { }

        public long GetMarkValue(long messageId) 
            => set.Where(m => m.MessageId == messageId).Sum(m => (long)m.Type);
    }
}

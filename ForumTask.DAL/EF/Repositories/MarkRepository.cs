using ForumTask.DAL.Entities;
using ForumTask.DAL.Interfaces;
using System.Linq;

namespace ForumTask.DAL.EF.Repositories {
    class MarkRepository : GenericRepository<Mark>, IMarkRepository {
        public MarkRepository(ForumContext context) : base(context) { }

        public long GetMarkValue(long messageId)
            => set.Where(m => m.MessageId == messageId).Sum(m => (long)m.Type);
    }
}

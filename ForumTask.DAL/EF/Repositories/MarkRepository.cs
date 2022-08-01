using System.Linq;
using ForumTask.DAL.Entities;
using ForumTask.DAL.Interfaces;

namespace ForumTask.DAL.EF.Repositories
{
    internal class MarkRepository : GenericRepository<Mark>, IMarkRepository
    {
        public MarkRepository(ForumContext context) : base(context) { }

        public long GetCountOfType(long messageId, sbyte type)
        {
            return set.LongCount(m => m.MessageId == messageId && (sbyte)m.Type == type);
        }
    }
}

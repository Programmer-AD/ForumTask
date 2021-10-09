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

        public int GetMarkValue(Message message) {
            if (message is null) throw new ArgumentNullException(nameof(message));
            return set.Where(m => m.MessageId == message.Id).Sum(m => (sbyte)m.Type);
        }
    }
}

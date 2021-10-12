using ForumTask.BLL.DTO;
using ForumTask.BLL.Interfaces;
using ForumTask.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumTask.BLL.Services {
    class MarkService : IMarkService {
        private readonly IUnitOfWork uow;
        public MarkService(IUnitOfWork uow) {
            this.uow = uow;
        }

        public sbyte GetOwn(uint userId, ulong messageId)
            =>((sbyte?)uow.Marks.Get(userId, messageId)?.Type) ?? 0;

        public long GetTotal(ulong messageId)
            => uow.Marks.GetMarkValue(messageId);

        public void Set(MarkDTO mark) {
            if (mark.Value == 0) {
                try {
                    uow.Marks.Delete(mark.UserId, mark.MessageId);
                } catch (InvalidOperationException) { }
            } else {
                var dbm = uow.Marks.Get(mark.UserId, mark.MessageId);
                if (dbm is null)
                    uow.Marks.Create(mark.ToEntity());
                else if ((sbyte)dbm.Type != mark.Value)
                    uow.Marks.Update(mark.ToEntity());
            }
            uow.SaveChanges();
        }
    }
}

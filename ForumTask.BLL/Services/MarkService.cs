using ForumTask.BLL.DTO;
using ForumTask.BLL.Interfaces;
using ForumTask.DAL.Interfaces;
using System;

namespace ForumTask.BLL.Services {
    class MarkService : IMarkService {
        private readonly IUnitOfWork uow;
        public MarkService(IUnitOfWork uow) {
            this.uow = uow;
        }

        public sbyte GetOwn(int userId, long messageId)
            => ((sbyte?)uow.Marks.Get(userId, messageId)?.Type) ?? 0;

        public long GetTotal(long messageId)
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
                else if ((sbyte)dbm.Type != mark.Value) {
                    dbm.Type = (DAL.Entities.MarkType)Math.Sign(mark.Value);
                    uow.Marks.Update(dbm);
                }
            }
            uow.SaveChanges();
        }
    }
}

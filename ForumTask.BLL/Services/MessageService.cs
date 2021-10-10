using ForumTask.BLL.DTO;
using ForumTask.BLL.Exceptions;
using ForumTask.BLL.Interfaces;
using ForumTask.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumTask.BLL.Services {
    class MessageService:IMessageService {
        private readonly IUnitOfWork uow;
        private readonly IUserService userServ;
        private readonly IMarkService markServ;

        public MessageService(IUnitOfWork uow,IUserService user,IMarkService mark) {
            this.uow = uow;
            userServ = user;
            markServ = mark;
        }

        private void CheckEditAccess(DateTime writeTime, uint? authorId, uint callerId, bool canEditOtherUser) {
            var user = userServ.Get(callerId);
            if (user.IsBanned)
                throw new AccessDeniedException("Caller is banned");
            if ((user.MaxRole == RoleEnum.User || !canEditOtherUser) && (!authorId.HasValue || authorId.Value != callerId))
                throw new AccessDeniedException("Not enough rights to edit/delete other users message");
            if (user.MaxRole == RoleEnum.User
                && (DateTime.UtcNow - writeTime).TotalMinutes > ITopicService.EditOrDeleteTime)
                throw new AccessDeniedException("Edit/delete time limit exceed");
            if (authorId.HasValue) {
                var cru = userServ.Get(authorId.Value);
                if (cru.MaxRole >= user.MaxRole)
                    throw new AccessDeniedException("Can`t edit/delete message of user with same or bigger role");
            }
        }

        public void Add(MessageDTO message) {
            var user = userServ.Get(message.AuthorId.Value);
            if (user.IsBanned)
                throw new AccessDeniedException("Caller is banned");
            if (uow.Topics.Get(message.TopicId) is null)
                throw new NotFoundException();

            message.WriteTime = DateTime.UtcNow;
            uow.Messages.Create(message.ToEntity());
        }

        public void Delete(ulong messageId, uint userId) {
            var msg = uow.Messages.Get(messageId) ?? throw new NotFoundException();
            CheckEditAccess(msg.WriteTime, msg.AuthorId, userId, true);
            uow.Messages.Delete(msg);
        }

        public void Edit(ulong messageId, string newText, uint userId) {
            var msg = uow.Messages.Get(messageId) ?? throw new NotFoundException();
            CheckEditAccess(msg.WriteTime, msg.AuthorId, userId, false);
            msg.Text = newText;
            uow.Messages.Update(msg);
        }

        public int GetMessageCount(ulong topicId)
            => uow.Messages.GetMessageCount(topicId);

        public IEnumerable<MessageDTO> GetTopOld(ulong topicId, uint page)
            => uow.Messages.GetTopOld(topicId, IMessageService.PageSize, IMessageService.PageSize * (int)page)
            .Select(m => new MessageDTO(m) { Mark = markServ.GetTotal(m.Id) });
    }
}

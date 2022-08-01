using System;
using System.Collections.Generic;
using System.Linq;
using ForumTask.BLL.DTO;
using ForumTask.BLL.Exceptions;
using ForumTask.BLL.Interfaces;
using ForumTask.DAL.Interfaces;

namespace ForumTask.BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserService userServ;
        private readonly IMarkService markServ;

        public MessageService(IUnitOfWork uow, IUserService user, IMarkService mark)
        {
            this.uow = uow;
            userServ = user;
            markServ = mark;
        }

        private void CheckEditAccess(DateTime writeTime, int? authorId, int callerId, bool canEditOtherUser)
        {
            var user = userServ.Get(callerId);
            if (user.IsBanned)
            {
                throw new AccessDeniedException("Caller is banned");
            }

            if ((user.MaxRole == RoleEnum.User || !canEditOtherUser) && (!authorId.HasValue || authorId.Value != callerId))
            {
                throw new AccessDeniedException("Not enough rights to edit/delete other users message");
            }

            if (user.MaxRole == RoleEnum.User
                && (DateTime.UtcNow - writeTime).TotalMinutes > ITopicService.EditOrDeleteTime)
            {
                throw new AccessDeniedException("Edit/delete time limit exceed");
            }

            if (authorId.HasValue && authorId.Value != callerId)
            {
                var cru = userServ.Get(authorId.Value);
                if (cru.MaxRole >= user.MaxRole)
                {
                    throw new AccessDeniedException("Can`t edit/delete message of user with same or bigger role");
                }
            }
        }

        public void Add(MessageDTO message)
        {
            var user = userServ.Get(message.AuthorId.Value);
            if (user.IsBanned)
            {
                throw new AccessDeniedException("Caller is banned");
            }

            if (uow.Topics.Get(message.TopicId) is null)
            {
                throw new NotFoundException();
            }

            message.WriteTime = DateTime.UtcNow;
            uow.Messages.Create(message.ToEntity());
            uow.SaveChanges();
        }

        public void Delete(long messageId, int userId)
        {
            var msg = uow.Messages.Get(messageId) ?? throw new NotFoundException();
            CheckEditAccess(msg.WriteTime, msg.AuthorId, userId, true);
            uow.Messages.Delete(msg);
            uow.SaveChanges();
        }

        public void Edit(long messageId, string newText, int userId)
        {
            var msg = uow.Messages.Get(messageId) ?? throw new NotFoundException();
            CheckEditAccess(msg.WriteTime, msg.AuthorId, userId, false);
            msg.Text = newText;
            uow.Messages.Update(msg);
            uow.SaveChanges();
        }

        public int GetMessageCount(long topicId)
        {
            return uow.Messages.GetMessageCount(topicId);
        }

        public IEnumerable<MessageDTO> GetTopOld(long topicId, int page)
        {
            return uow.Messages.GetTopOld(topicId, IMessageService.PageSize, IMessageService.PageSize * page)
                    .ToList().Select(m => new MessageDTO(m)
                    {
                        PositiveCount = markServ.GetCountOfType(m.Id, 1),
                        NegativeCount = markServ.GetCountOfType(m.Id, -1)
                    });
        }

        void IMessageService.Add(MessageDTO message, DAL.Entities.Topic topic)
        {
            var ent = message.ToEntity();
            ent.Topic = topic;
            uow.Messages.Create(ent);
        }

        public int GetPagesCount(long topicId)
        {
            int cnt = uow.Messages.GetMessageCount(topicId);
            return cnt == 0 ? 0 : (cnt / IMessageService.PageSize + 1);
        }
    }
}

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
    class TopicService:ITopicService {
        private readonly IUnitOfWork uow;
        private readonly IMessageService msgServ;
        private readonly IUserService userServ;

        public TopicService(IUnitOfWork uow, IMessageService msg, IUserService user) {
            this.uow = uow;
            msgServ = msg;
            userServ = user;
        }

        private void CheckEditAccess(DateTime createTime,uint? creatorId,uint callerId,bool canEditOtherUser) {
            var user = userServ.Get(callerId);
            if (user.IsBanned)
                throw new AccessDeniedException("Caller is banned");
            if ((user.MaxRole == RoleEnum.User||!canEditOtherUser) && (!creatorId.HasValue || creatorId.Value != callerId))
                throw new AccessDeniedException("Not enough rights to edit/delete other users topic");
            if (user.MaxRole == RoleEnum.User
                && (DateTime.UtcNow - createTime).TotalMinutes > ITopicService.EditOrDeleteTime)
                throw new AccessDeniedException("Edit/delete time limit exceed");
            if (creatorId.HasValue) {
                var cru = userServ.Get(creatorId.Value);
                if (cru.MaxRole >= user.MaxRole)
                    throw new AccessDeniedException("Can`t edit/delete topic of user with same or bigger role");
            }
        }

        public ulong Create(string title, string message, uint creatorId) {
            var user = userServ.Get(creatorId);
            if (user.IsBanned)
                throw new AccessDeniedException("Caller is banned");

            var t = new DAL.Entities.Topic() {
                CreateTime = DateTime.UtcNow,
                CreatorId = creatorId,
                Title = title
            };
            uow.Topics.Create(t);
            if (!string.IsNullOrEmpty(message))
                msgServ.Add(new() { 
                    AuthorId=creatorId, 
                    Text=message, 
                    WriteTime=DateTime.UtcNow,
                }, t);
            uow.SaveChanges();
            return t.Id;
        }

        public TopicDTO Get(ulong id) {
            var t = uow.Topics.Get(id)??throw new NotFoundException();
            return new TopicDTO(t) { MessageCount = msgServ.GetMessageCount(id) };
        }
        public int GetPagesCount()
            => uow.Topics.Count() / ITopicService.PageSize;

        public IEnumerable<TopicDTO> GetTopNew(uint page, string searchTitle = "") 
            =>uow.Topics.GetTopNew(ITopicService.PageSize, (int)page * ITopicService.PageSize, searchTitle)
                .Select(t => new TopicDTO(t) { 
                    MessageCount = msgServ.GetMessageCount(t.Id) 
                });

        public void Rename(ulong topicId, string newTitle, uint userId) {
            var topic = uow.Topics.Get(topicId)?? throw new NotFoundException();
            CheckEditAccess(topic.CreateTime, topic.CreatorId, userId, false);
            topic.Title = newTitle;
            uow.Topics.Update(topic);
            uow.SaveChanges();
        }
        public void Delete(ulong topicId, uint userId) {
            var topic = uow.Topics.Get(topicId) ?? throw new NotFoundException();
            CheckEditAccess(topic.CreateTime, topic.CreatorId, userId, true);
            uow.Topics.Delete(topic);
            uow.SaveChanges();
        }
    }
}

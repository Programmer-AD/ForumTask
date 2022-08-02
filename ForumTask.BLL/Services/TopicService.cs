using ForumTask.BLL.DTO;
using ForumTask.BLL.Exceptions;
using ForumTask.BLL.Interfaces;
using ForumTask.DAL.Entities;
using ForumTask.DAL.Interfaces;

namespace ForumTask.BLL.Services
{
    public class TopicService : ITopicService
    {
        private readonly IRepository<Topic> topicRepository;
        private readonly IRepository<Message> messageRepository;
        private readonly IUserService userService;

        public TopicService(
            IRepository<Topic> topicRepository,
            IRepository<Message> messageRepository,
            IUserService userService)
        {
            this.topicRepository = topicRepository;
            this.messageRepository = messageRepository;
            this.userService = userService;
        }

        public async Task<long> CreateAsync(string title, string messageText, long callerId)
        {
            var caller = await userService.GetAsync(callerId);

            if (caller.IsBanned)
            {
                throw new AccessDeniedException("Caller is banned");
            }

            var topic = new Topic()
            {
                CreateTime = DateTime.UtcNow,
                CreatorId = callerId,
                Title = title
            };

            await topicRepository.CreateAsync(topic);

            if (!string.IsNullOrEmpty(messageText))
            {
                var message = new Message()
                {
                    AuthorId = callerId,
                    Text = messageText,
                    WriteTime = DateTime.UtcNow,
                    Topic = topic
                };

                await messageRepository.CreateAsync(message);
            }

            return topic.Id;
        }

        public async Task<TopicDTO> GetAsync(long id)
        {
            var topic = await GetTopicByIdAsync(id);

            var messageCount = await GetMessageCountAsync(id);
            var topicDto = new TopicDTO(topic) { MessageCount = messageCount };

            return topicDto;
        }

        public async Task<int> GetPagesCountAsync()
        {
            var count = await topicRepository.CountAsync();

            var result = count == 0 ? 0 : (int)(count / ITopicService.PageSize + 1); ;

            return result;
        }

        public async Task<IEnumerable<TopicDTO>> GetTopNewAsync(int page, string searchTitle = "")
        {
            var topics = await topicRepository.GetAllAsync(
                predicate: x => x.Title.Contains(searchTitle),
                orderFunc: x => x.OrderByDescending(x => x.CreateTime),
                skipCount: page * ITopicService.PageSize,
                takeCount: ITopicService.PageSize);

            var topicDtos = topics.Select(x => new TopicDTO(x)).ToArray();

            foreach (var topicDto in topicDtos)
            {
                topicDto.MessageCount = await GetMessageCountAsync(topicDto.Id);
            }

            return topicDtos;
        }

        public async Task RenameAsync(long topicId, string newTitle, long callerId)
        {
            var topic = await GetTopicByIdAsync(topicId);

            await CheckEditAccessAsync(topic.CreateTime, topic.CreatorId, callerId, false);

            topic.Title = newTitle;
            await topicRepository.UpdateAsync(topic);
        }

        public async Task DeleteAsync(long topicId, long callerId)
        {
            var topic = await GetTopicByIdAsync(topicId);

            await CheckEditAccessAsync(topic.CreateTime, topic.CreatorId, callerId, true);

            await topicRepository.DeleteAsync(topic);
        }

        private async Task<Topic> GetTopicByIdAsync(long topicId)
        {
            var topic = await topicRepository.GetAsync(x => x.Id == topicId) ?? throw new NotFoundException();

            return topic;
        }

        private Task<long> GetMessageCountAsync(long topicId)
        {
            return messageRepository.CountAsync(x => x.TopicId == topicId);
        }

        private async Task CheckEditAccessAsync(DateTime createTime, long? creatorId, long callerId, bool canEditOtherUser)
        {
            var user = await userService.GetAsync(callerId);
            if (user.IsBanned)
            {
                throw new AccessDeniedException("Caller is banned");
            }

            if ((user.MaxRole == RoleEnum.User || !canEditOtherUser) && (!creatorId.HasValue || creatorId.Value != callerId))
            {
                throw new AccessDeniedException("Not enough rights to edit/delete other users topic");
            }

            if (user.MaxRole == RoleEnum.User
                && (DateTime.UtcNow - createTime).TotalMinutes > ITopicService.EditOrDeleteTime)
            {
                throw new AccessDeniedException("Edit/delete time limit exceed");
            }

            if (creatorId.HasValue && creatorId.Value != callerId)
            {
                var creator = await userService.GetAsync(creatorId.Value);

                if (creator.MaxRole >= user.MaxRole)
                {
                    throw new AccessDeniedException("Can`t edit/delete topic of user with same or bigger role");
                }
            }
        }
    }
}

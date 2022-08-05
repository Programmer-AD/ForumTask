using AutoMapper;
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
        private readonly IMapper mapper;

        public TopicService(
            IRepository<Topic> topicRepository,
            IRepository<Message> messageRepository,
            IUserService userService,
            IMapper mapper)
        {
            this.topicRepository = topicRepository;
            this.messageRepository = messageRepository;
            this.userService = userService;
            this.mapper = mapper;
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

            await topicRepository.ForceSaveChangesAsync();

            return topic.Id;
        }

        public async Task<TopicDto> GetAsync(long id)
        {
            var topic = await GetTopicByIdAsync(id);

            var messageCount = await GetMessageCountAsync(id);

            var topicDto = mapper.Map<TopicDto>(topic);
            topicDto.MessageCount = messageCount;

            return topicDto;
        }

        public async Task<int> GetPagesCountAsync()
        {
            var count = await topicRepository.CountAsync();

            int result = count == 0 ? 0 : (int)(count / ITopicService.PageSize + 1); ;

            return result;
        }

        public async Task<IEnumerable<TopicDto>> GetTopNewAsync(int page, string searchTitle = "")
        {
            var topics = await topicRepository.GetAllAsync(
                predicate: x => x.Title.Contains(searchTitle),
                orderFunc: x => x.OrderByDescending(x => x.CreateTime),
                skipCount: page * ITopicService.PageSize,
                takeCount: ITopicService.PageSize);

            var topicDtos = mapper.Map<IEnumerable<TopicDto>>(topics);

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

            var isRegularUser = user.MaxRole == RoleEnum.User;
            var hasCreator = creatorId.HasValue;
            var notOwn = creatorId != callerId;

            if ((isRegularUser || !canEditOtherUser) && notOwn)
            {
                throw new AccessDeniedException("Not enough rights to edit/delete other users topic");
            }

            var timeSinceCreate = DateTime.UtcNow - createTime;
            var timeLimitExceed = timeSinceCreate.TotalMinutes > ITopicService.EditOrDeleteTime;

            if (isRegularUser && timeLimitExceed)
            {
                throw new AccessDeniedException("Edit/delete time limit exceed");
            }

            if (!isRegularUser && hasCreator && notOwn)
            {
                var creator = await userService.GetAsync(creatorId.Value);

                if (user.MaxRole <= creator.MaxRole)
                {
                    throw new AccessDeniedException("Can`t edit/delete topic of user with same or bigger role");
                }
            }
        }
    }
}

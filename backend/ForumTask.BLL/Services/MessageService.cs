using AutoMapper;
using ForumTask.BLL.DTO;
using ForumTask.BLL.Exceptions;
using ForumTask.BLL.Interfaces;
using ForumTask.DAL.Entities;
using ForumTask.DAL.Interfaces;

namespace ForumTask.BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<Message> messageRepository;
        private readonly IRepository<Topic> topicRepository;
        private readonly IRepository<Mark> markRepository;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public MessageService(
            IRepository<Message> messageRepository,
            IRepository<Topic> topicRepository,
            IRepository<Mark> markRepository,
            IUserService userService,
            IMapper mapper)
        {
            this.messageRepository = messageRepository;
            this.topicRepository = topicRepository;
            this.markRepository = markRepository;
            this.userService = userService;
            this.mapper = mapper;
        }

        public async Task AddAsync(MessageDto messageDto)
        {
            var user = await userService.GetAsync(messageDto.AuthorId.Value);
            if (user.IsBanned)
            {
                throw new AccessDeniedException("Caller is banned");
            }

            var topic = await topicRepository.GetAsync(x => x.Id == messageDto.TopicId) ?? throw new NotFoundException();

            messageDto.WriteTime = DateTime.UtcNow;

            var message = mapper.Map<Message>(messageDto);

            await messageRepository.CreateAsync(message);
        }

        public async Task DeleteAsync(long messageId, long callerId)
        {
            var message = await GetMessageById(messageId);

            await CheckEditAccessAsync(message.WriteTime, message.AuthorId, callerId, true);

            await messageRepository.DeleteAsync(message);
        }

        public async Task EditAsync(long messageId, string newText, long callerId)
        {
            var message = await GetMessageById(messageId);

            await CheckEditAccessAsync(message.WriteTime, message.AuthorId, callerId, true);

            message.Text = newText;
            await messageRepository.UpdateAsync(message);
        }

        public async Task<IEnumerable<MessageDto>> GetTopOldAsync(long topicId, int page)
        {
            var messages = await messageRepository.GetAllAsync(
                predicate: x => x.TopicId == topicId,
                orderFunc: x => x.OrderBy(x => x.WriteTime),
                skipCount: IMessageService.PageSize * page,
                takeCount: IMessageService.PageSize);

            var messageDtos = mapper.Map<IEnumerable<MessageDto>>(messages);

            foreach (var messageDto in messageDtos)
            {
                messageDto.PositiveCount = await GetCountOfMarksAsync(messageDto.Id, MarkType.Positive);
                messageDto.NegativeCount = await GetCountOfMarksAsync(messageDto.Id, MarkType.Negative);
            }

            return messageDtos;
        }

        public async Task<int> GetPagesCountAsync(long topicId)
        {
            var count = await messageRepository.CountAsync(x => x.TopicId == topicId);

            int result = count == 0 ? 0 : (int)(count / IMessageService.PageSize + 1);

            return result;
        }

        private async Task<Message> GetMessageById(long messageId)
        {
            var message = await messageRepository.GetAsync(x => x.Id == messageId) ?? throw new NotFoundException();

            return message;
        }

        private async Task CheckEditAccessAsync(DateTime writeTime, long? authorId, long callerId, bool canEditOtherUser)
        {
            var user = await userService.GetAsync(callerId);

            if (user.IsBanned)
            {
                throw new AccessDeniedException("Caller is banned");
            }

            var isRegularUser = user.MaxRole == RoleEnum.User;
            var hasAuthor = authorId.HasValue;
            var notOwn = authorId != callerId;

            if ((isRegularUser || !canEditOtherUser) && notOwn)
            {
                throw new AccessDeniedException("Not enough rights to edit/delete other users message");
            }

            var timeSinceWrite = DateTime.UtcNow - writeTime;
            var timeLimitExceed = timeSinceWrite.TotalMinutes > ITopicService.EditOrDeleteTime;

            if (isRegularUser && timeLimitExceed)
            {
                throw new AccessDeniedException("Edit/delete time limit exceed");
            }

            if (!isRegularUser && hasAuthor && notOwn)
            {
                var author = await userService.GetAsync(authorId.Value);

                if (user.MaxRole <= author.MaxRole)
                {
                    throw new AccessDeniedException("Can`t edit/delete message of user with same or bigger role");
                }
            }
        }

        private Task<long> GetCountOfMarksAsync(long messageId, MarkType markType)
        {
            return markRepository.CountAsync(x => x.MessageId == messageId && x.Type == markType);
        }
    }
}

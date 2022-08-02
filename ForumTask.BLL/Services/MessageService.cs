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

        public MessageService(
            IRepository<Message> messageRepository,
            IRepository<Topic> topicRepository,
            IRepository<Mark> markRepository,
            IUserService userService)
        {
            this.messageRepository = messageRepository;
            this.topicRepository = topicRepository;
            this.markRepository = markRepository;
            this.userService = userService;
        }

        public async Task AddAsync(MessageDto message)
        {
            var user = await userService.GetAsync(message.AuthorId.Value);
            if (user.IsBanned)
            {
                throw new AccessDeniedException("Caller is banned");
            }

            var topic = await topicRepository.GetAsync(x => x.Id == message.TopicId) ?? throw new NotFoundException();

            message.WriteTime = DateTime.UtcNow;
            await messageRepository.CreateAsync(message.ToEntity());
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

            var messageDtos = messages.Select(x => new MessageDto(x)).ToArray();

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

            var result = count == 0 ? 0 : (int)(count / IMessageService.PageSize + 1);

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
                var author = await userService.GetAsync(authorId.Value);

                if (author.MaxRole >= user.MaxRole)
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

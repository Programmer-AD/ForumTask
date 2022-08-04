using AutoMapper;
using ForumTask.BLL.DTO;
using ForumTask.BLL.Interfaces;
using ForumTask.DAL.Entities;
using ForumTask.DAL.Interfaces;

namespace ForumTask.BLL.Services
{
    public class MarkService : IMarkService
    {
        private readonly IRepository<Mark> markRepository;
        private readonly IMapper mapper;

        public MarkService(
            IRepository<Mark> markRepository,
            IMapper mapper)
        {
            this.markRepository = markRepository;
            this.mapper = mapper;
        }

        public async Task<sbyte> GetOwnAsync(long userId, long messageId)
        {
            var mark = await GetMarkByKeysAsync(userId, messageId);

            var result = ((sbyte?)mark?.Type) ?? 0;

            return result;
        }

        public async Task SetAsync(MarkDto markDto)
        {
            var mark = await GetMarkByKeysAsync(markDto.UserId, markDto.MessageId);

            if (markDto.Value == 0)
            {
                if (mark != null)
                {
                    await markRepository.DeleteAsync(mark);
                }
            }
            else
            {
                var newMarkType = (MarkType)Math.Sign(markDto.Value);

                if (mark == null)
                {
                    var newMark = mapper.Map<Mark>(markDto);

                    await markRepository.CreateAsync(newMark);
                }
                else if (mark.Type != newMarkType)
                {
                    mark.Type = newMarkType;

                    await markRepository.UpdateAsync(mark);
                }
            }
        }

        private Task<Mark> GetMarkByKeysAsync(long userId, long messageId)
        {
            return markRepository.GetAsync(x => x.UserId == userId && x.MessageId == messageId);
        }
    }
}

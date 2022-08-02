using ForumTask.BLL.DTO;
using ForumTask.BLL.Interfaces;
using ForumTask.DAL.Entities;
using ForumTask.DAL.Interfaces;

namespace ForumTask.BLL.Services
{
    public class MarkService : IMarkService
    {
        private readonly IRepository<Mark> markRepository;

        public MarkService(IRepository<Mark> markRepository)
        {
            this.markRepository = markRepository;
        }

        public async Task<sbyte> GetOwnAsync(long userId, long messageId)
        {
            var mark = await GetMarkByKeys(userId, messageId);

            return ((sbyte?)mark?.Type) ?? 0;
        }

        public async Task SetAsync(MarkDTO markDto)
        {
            var mark = await GetMarkByKeys(markDto.UserId, markDto.UserId);

            if (markDto.Value == 0)
            {
                if (mark != null)
                {
                    await markRepository.DeleteAsync(mark);
                }
            }
            else
            {
                if (mark == null)
                {
                    var newMark = markDto.ToEntity();

                    await markRepository.CreateAsync(newMark);
                }
                else if ((sbyte)mark.Type != markDto.Value)
                {
                    mark.Type = (MarkType)Math.Sign(markDto.Value);

                    await markRepository.UpdateAsync(mark);
                }
            }
        }

        private Task<Mark> GetMarkByKeys(long userId, long messageId)
        {
            return markRepository.GetAsync(x => x.UserId == userId && x.MessageId == messageId);
        }
    }
}

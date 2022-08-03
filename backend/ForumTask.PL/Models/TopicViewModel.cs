using ForumTask.BLL.DTO;

namespace ForumTask.PL.Models
{
    public class TopicViewModel
    {
        public TopicViewModel() { }
        public TopicViewModel(TopicDto dto)
        {
            Id = dto.Id;
            Title = dto.Title;
            CreateTime = dto.CreateTime;
            CreatorId = dto.CreatorId;
            MessageCount = dto.MessageCount;
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateTime { get; set; }
        public long? CreatorId { get; set; }
        public long MessageCount { get; set; }
    }
}

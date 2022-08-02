using ForumTask.DAL.Entities;

namespace ForumTask.BLL.DTO
{
    public class TopicDto
    {
        public TopicDto() { }
        public TopicDto(Topic topic)
        {
            Id = topic.Id;
            Title = topic.Title;
            CreateTime = topic.CreateTime;
            CreatorId = topic.CreatorId;
        }
        public Topic ToEntity()
        {
            return new()
            {
                Id = Id,
                CreatorId = CreatorId,
                Title = Title,
                CreateTime = CreateTime
            };
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateTime { get; set; }
        public long? CreatorId { get; set; }

        public long MessageCount { get; set; }
    }
}

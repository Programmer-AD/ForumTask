namespace ForumTask.PL.Models
{
    public class TopicViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateTime { get; set; }
        public long? CreatorId { get; set; }
        public long MessageCount { get; set; }
    }
}

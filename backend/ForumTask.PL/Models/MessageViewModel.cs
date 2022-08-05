namespace ForumTask.PL.Models
{
    public class MessageViewModel
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public long TopicId { get; set; }
        public long? AuthorId { get; set; }
        public DateTime WriteTime { get; set; }

        public long PositiveCount { get; set; }
        public long NegativeCount { get; set; }
    }
}

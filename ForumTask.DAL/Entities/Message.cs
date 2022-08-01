namespace ForumTask.DAL.Entities
{
    public class Message
    {
        public long Id { get; set; }

        public string Text { get; set; }

        public DateTime WriteTime { get; set; }

        public long TopicId { get; set; }
        public Topic Topic { get; set; }

        public long? AuthorId { get; set; }
        public User Author { get; set; }
    }
}

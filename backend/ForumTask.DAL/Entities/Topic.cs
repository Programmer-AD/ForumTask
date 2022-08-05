namespace ForumTask.DAL.Entities
{
    public class Topic
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public DateTime CreateTime { get; set; }

        public long? CreatorId { get; set; }
        public User Creator { get; set; }

        public List<Message> Messages { get; set; } = new();
    }
}

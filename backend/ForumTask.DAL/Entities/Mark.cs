namespace ForumTask.DAL.Entities
{
    public enum MarkType : sbyte
    {
        Negative = -1, Positive = 1
    }
    public class Mark
    {
        //Use (userId, messageId) as key
        public MarkType Type { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }

        public long MessageId { get; set; }
        public Message Message { get; set; }
    }
}

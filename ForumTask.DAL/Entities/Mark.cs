namespace ForumTask.DAL.Entities {
    public enum MarkType {
        Negative = -1, Positive = 1
    }
    public class Mark {
        //Will use (userId, messageId) as key
        public MarkType Type { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public long MessageId { get; set; }
        public Message Message { get; set; }
    }
}

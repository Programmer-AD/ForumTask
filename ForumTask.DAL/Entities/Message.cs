using System;
using System.ComponentModel.DataAnnotations;


namespace ForumTask.DAL.Entities {
    public class Message {
        public long Id { get; set; }

        [MaxLength(5000)]
        public string Text { get; set; }

        public DateTime WriteTime { get; set; }

        public long TopicId { get; set; }
        public Topic Topic { get; set; }

        public int? AuthorId { get; set; }
        public User Author { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ForumTask.DAL.Entities {
    public class Message {
        public ulong Id { get; set; }

        [MaxLength(5000)]
        public string Text { get; set; }
        
        public DateTime WriteTime { get; set; }

        public Topic Topic { get; set; }
        public ulong TopicId { get; set; }

        public uint? AuthorId { get; set; }
        public User Author { get; set; }
    }
}

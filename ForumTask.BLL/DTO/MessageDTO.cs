using ForumTask.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumTask.BLL.DTO {
    public class MessageDTO {
        public MessageDTO() { }
        public MessageDTO(Message msg) {
            Id = msg.Id;
            Text = msg.Text;
            TopicId = msg.TopicId;
            AuthorId = msg.AuthorId;
            WriteTime = msg.WriteTime;
        }
        public Message ToEntity()
            => new() {
                AuthorId=AuthorId,
                Id=Id,
                WriteTime=WriteTime,
                TopicId=TopicId,
                Text=Text
            };

        public long Id { get; set; }
        public string Text { get; set; }
        public long TopicId { get; set; }
        public int? AuthorId { get; set; }
        public DateTime WriteTime { get; set; }

        public long Mark { get; set; }
    }
}

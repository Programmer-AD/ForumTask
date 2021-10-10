using ForumTask.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumTask.PL.Models {
    public class MessageViewModel {
        public MessageViewModel() { }
        public MessageViewModel(MessageDTO dto) {
            Id = dto.Id;
            Text = dto.Text;
            TopicId = dto.TopicId;
            AuthorId = dto.AuthorId;
            WriteTime = dto.WriteTime;
            Mark = dto.Mark;
        }

        public ulong Id { get; set; }
        public string Text { get; set; }
        public ulong TopicId { get; set; }
        public uint? AuthorId { get; set; }
        public DateTime WriteTime { get; set; }
        public long Mark { get; set; }
    }
}

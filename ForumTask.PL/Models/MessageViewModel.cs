using ForumTask.BLL.DTO;
using System;

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

        public long Id { get; set; }
        public string Text { get; set; }
        public long TopicId { get; set; }
        public int? AuthorId { get; set; }
        public DateTime WriteTime { get; set; }
        public long Mark { get; set; }
    }
}

using System;
using ForumTask.BLL.DTO;

namespace ForumTask.PL.Models
{
    public class MessageViewModel
    {
        public MessageViewModel() { }
        public MessageViewModel(MessageDto dto)
        {
            Id = dto.Id;
            Text = dto.Text;
            TopicId = dto.TopicId;
            AuthorId = dto.AuthorId;
            WriteTime = dto.WriteTime;
            PositiveCount = dto.PositiveCount;
            NegativeCount = dto.NegativeCount;
        }

        public long Id { get; set; }
        public string Text { get; set; }
        public long TopicId { get; set; }
        public int? AuthorId { get; set; }
        public DateTime WriteTime { get; set; }
        public long PositiveCount { get; set; }
        public long NegativeCount { get; set; }
    }
}

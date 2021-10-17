using System;
using ForumTask.DAL.Entities;

namespace ForumTask.BLL.DTO {
    public class MarkDTO {
        public MarkDTO() { }
        public MarkDTO(Mark mark) {
            UserId = mark.UserId;
            MessageId = mark.MessageId;
            Value = (sbyte)mark.Type;
        }
        public Mark ToEntity()
            => new() {
                UserId = UserId,
                MessageId = MessageId,
                Type = (MarkType)Math.Sign(Value)
            };

        public int UserId { get; set; }
        public long MessageId { get; set; }
        public sbyte Value { get; set; }
    }
}

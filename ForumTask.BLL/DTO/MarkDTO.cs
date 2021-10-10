using ForumTask.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public uint UserId { get; set; }
        public ulong MessageId { get; set; }
        public sbyte Value { get; set; }
    }
}

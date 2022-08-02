using ForumTask.DAL.Entities;

namespace ForumTask.BLL.DTO
{
    public class MarkDto
    {
        public MarkDto() { }
        public MarkDto(Mark mark)
        {
            UserId = mark.UserId;
            MessageId = mark.MessageId;
            Value = (sbyte)mark.Type;
        }
        public Mark ToEntity()
        {
            return new()
            {
                UserId = UserId,
                MessageId = MessageId,
                Type = (MarkType)Math.Sign(Value)
            };
        }

        public long UserId { get; set; }
        public long MessageId { get; set; }
        public sbyte Value { get; set; }
    }
}

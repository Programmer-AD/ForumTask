using System.ComponentModel.DataAnnotations;
using ForumTask.DAL.Entities;

namespace ForumTask.PL.Models
{
    public class MessageAddModel
    {
        [Required]
        [MaxLength(EntityConstants.Message_Text_MaxLength)]
        public string Text { get; set; }

        public long TopicId { get; set; }
    }
}

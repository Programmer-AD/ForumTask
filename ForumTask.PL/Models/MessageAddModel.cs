using System.ComponentModel.DataAnnotations;

namespace ForumTask.PL.Models
{
    public class MessageAddModel
    {
        [Required]
        [MaxLength(5000)]
        public string Text { get; set; }

        public long TopicId { get; set; }
    }
}

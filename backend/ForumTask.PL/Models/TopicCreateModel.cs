using System.ComponentModel.DataAnnotations;
using ForumTask.DAL.Entities;

namespace ForumTask.PL.Models
{
    public class TopicCreateModel
    {
        [Required]
        [MinLength(EntityConstants.Topic_Title_MinLength)]
        [MaxLength(EntityConstants.Topic_Title_MaxLength)]
        public string Title { get; set; }

        [MaxLength(EntityConstants.Message_Text_MaxLength)]
        public string Message { get; set; }
    }
}

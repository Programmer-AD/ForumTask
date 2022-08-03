using System.ComponentModel.DataAnnotations;
using ForumTask.DAL.Entities;

namespace ForumTask.PL.Models
{
    public class TopicRenameModel
    {
        [Required]
        [MinLength(EntityConstants.Topic_Title_MinLength)]
        [MaxLength(EntityConstants.Topic_Title_MaxLength)]
        public string NewTitle { get; set; }
    }
}

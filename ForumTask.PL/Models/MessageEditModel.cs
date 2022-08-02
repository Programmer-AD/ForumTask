using System.ComponentModel.DataAnnotations;
using ForumTask.DAL.Entities;

namespace ForumTask.PL.Models
{
    public class MessageEditModel
    {
        [Required]
        [MaxLength(EntityConstants.Message_Text_MaxLength)]
        public string NewText { get; set; }
    }
}

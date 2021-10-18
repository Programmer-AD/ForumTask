using System.ComponentModel.DataAnnotations;

namespace ForumTask.PL.Models {
    public class MessageEditModel {
        [Required]
        [MaxLength(5000)]
        public string NewText { get; set; }
    }
}

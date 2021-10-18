using System.ComponentModel.DataAnnotations;

namespace ForumTask.PL.Models {
    public class TopicCreateModel {
        [Required]
        [MaxLength(60)]
        [MinLength(5)]
        public string Title { get; set; }
        [MaxLength(5000)]
        public string Message { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ForumTask.PL.Models {
    public class TopicRenameModel {
        [MaxLength(60)]
        public string NewTitle { get; set; }
    }
}

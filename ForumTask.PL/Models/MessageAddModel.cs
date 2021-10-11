using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumTask.PL.Models {
    public class MessageAddModel {
        [Required]
        [MaxLength(5000)]
        public string Text { get; set; }

        public ulong TopicId { get; set; }
    }
}

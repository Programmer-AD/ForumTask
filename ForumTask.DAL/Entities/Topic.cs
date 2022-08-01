using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForumTask.DAL.Entities
{
    public class Topic
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string Title { get; set; }

        public DateTime CreateTime { get; set; }

        public int? CreatorId { get; set; }
        public User Creator { get; set; }

        public List<Message> Messages { get; set; } = new();
    }
}

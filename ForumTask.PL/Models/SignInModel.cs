using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumTask.PL.Models {
    public class SignInModel {
        [Required]
        [MaxLength(256)]
        public string UserName { get; set; }
        [MinLength(6)]
        [MaxLength(32)]
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}

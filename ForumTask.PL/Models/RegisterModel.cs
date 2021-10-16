using System.ComponentModel.DataAnnotations;

namespace ForumTask.PL.Models {
    public class RegisterModel {
        [Required]
        [MaxLength(256)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }
        [MinLength(6)]
        [MaxLength(32)]
        public string Password { get; set; }
    }
}

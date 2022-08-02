using System.ComponentModel.DataAnnotations;
using ForumTask.DAL.Entities;

namespace ForumTask.PL.Models
{
    public class RegisterModel
    {
        [Required]
        [MaxLength(EntityConstants.User_UserName_MaxLength)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(EntityConstants.User_Email_MaxLength)]
        public string Email { get; set; }

        [MinLength(EntityConstants.User_Password_MinLength)]
        [MaxLength(EntityConstants.User_Password_MaxLength)]
        public string Password { get; set; }
    }
}

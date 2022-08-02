using System.ComponentModel.DataAnnotations;
using ForumTask.DAL.Entities;

namespace ForumTask.PL.Models
{
    public class SignInModel
    {
        [Required]
        [MaxLength(EntityConstants.User_UserName_MaxLength)]
        public string UserName { get; set; }

        [MinLength(EntityConstants.User_Password_MinLength)]
        [MaxLength(EntityConstants.User_Password_MaxLength)]
        public string Password { get; set; }

        public bool Remember { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ForumTask.PL.Models {
    public class UserRoleSetModel {
        [Required]
        public string RoleName { get; set; }
        public bool SetHasRole { get; set; }
    }
}

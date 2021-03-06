using System;
using ForumTask.BLL.DTO;

namespace ForumTask.PL.Models {
    public class UserViewModel {
        public UserViewModel() { }
        public UserViewModel(UserDTO dto) {
            Id = dto.Id;
            UserName = dto.UserName;
            RegisterDate = dto.RegisterDate;
            IsBanned = dto.IsBanned;
            RoleName = dto.MaxRole.GetRoleName();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsBanned { get; set; }
        public string RoleName { get; set; }
    }
}

using ForumTask.DAL.Entities;
using System;

namespace ForumTask.BLL.DTO {
    public class UserDTO {
        public UserDTO() { }
        public UserDTO(User user) {
            Id = user.Id;
            UserName = user.UserName;
            Email = user.Email;
            RegisterDate = user.RegisterDate;
            IsBanned = user.IsBanned;
        }
        public User ToEntity()
            => new() {
                Id = Id,
                UserName = UserName,
                Email = Email,
                RegisterDate = RegisterDate,
                IsBanned = IsBanned,
            };

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsBanned { get; set; }

        public RoleEnum MaxRole { get; set; }
    }
}

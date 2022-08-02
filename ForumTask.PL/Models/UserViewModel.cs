using System;
using ForumTask.BLL.DTO;

namespace ForumTask.PL.Models
{
    public class UserViewModel
    {
        public UserViewModel() { }
        public UserViewModel(UserDto dto)
        {
            Id = dto.Id;
            UserName = dto.UserName;
            RegisterDate = dto.RegisterDate;
            IsBanned = dto.IsBanned;
            RoleName = dto.MaxRole.ToString();
        }

        public long Id { get; set; }
        public string UserName { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsBanned { get; set; }
        public string RoleName { get; set; }
    }
}

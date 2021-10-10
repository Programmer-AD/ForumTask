using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumTask.BLL.DTO {
    public enum RoleEnum {
        User=0, Moderator=1,Admin=2
    }
    static class RoleEnumConverter {
        /// <summary>
        /// Gets program role name by enum
        /// </summary>
        /// <param name="role">Role wich name to get</param>
        /// <returns>Role name</returns>
        public static string GetRoleName(this RoleEnum role)
            => role switch {
                RoleEnum.User=>"User",
                RoleEnum.Moderator=>"Moderator",
                RoleEnum.Admin=>"Admin",
                _=>"!unknown!"
            };
        /// <summary>
        /// Gets roleEnum by role name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static RoleEnum GetRoleByName(string name)
            => name.ToLower() switch {
                "user" => RoleEnum.User,
                "moderator" => RoleEnum.Moderator,
                "admin" => RoleEnum.Admin,
                _ => throw new ArgumentException("There is no role with providden name")
            };
    }
}

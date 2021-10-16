using System;

namespace ForumTask.BLL.DTO {
    public enum RoleEnum {
        User = 0, Moderator = 1, Admin = 2
    }
    public static class RoleEnumConverter {
        /// <summary>
        /// Gets program role name by enum
        /// </summary>
        /// <param name="role">Role wich name to get</param>
        /// <returns>Role name</returns>
        public static string GetRoleName(this RoleEnum role)
            => role switch {
                RoleEnum.User => "User",
                RoleEnum.Moderator => "Moderator",
                RoleEnum.Admin => "Admin",
                _ => "!unknown!"
            };
        /// <summary>
        /// Gets roleEnum by role name
        /// <para>
        /// If no role with such name, throw <see cref="ArgumentException"/>
        /// </para>
        /// </summary>
        /// <param name="name">Name of rol</param>
        /// <returns>RoleEnum value</returns>
        /// <exception cref="ArgumentException"/>
        public static RoleEnum GetRoleByName(string name)
            => name.ToLower() switch {
                "user" => RoleEnum.User,
                "moderator" => RoleEnum.Moderator,
                "admin" => RoleEnum.Admin,
                _ => throw new ArgumentException("There is no role with providden name")
            };
    }
}

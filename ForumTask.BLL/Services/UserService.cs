using System;
using System.Linq;
using ForumTask.BLL.DTO;
using ForumTask.BLL.Exceptions;
using ForumTask.BLL.Interfaces;

namespace ForumTask.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IIdentityManager man;

        public UserService(IIdentityManager man)
        {
            this.man = man;
        }

        private RoleEnum GetMaxRole(DAL.Entities.User user)
        {
            var roles = man.GetRoles(user);
            return roles.Select(s => RoleEnumConverter.GetRoleByName(s)).Max();
        }

        private void CheckRight(DAL.Entities.User victim, int callerId)
        {
            var caller = Get(callerId);
            if (caller.MaxRole <= GetMaxRole(victim))
            {
                throw new AccessDeniedException("To edit/delete this user your right-level must be greater then of that user");
            }
        }

        public void Delete(int userId, int callerId)
        {
            var user = man.FindById(userId) ??
                 throw new NotFoundException();
            if (userId != callerId)
            {
                CheckRight(user, callerId);
            }

            man.Delete(user);
        }

        public UserDTO Get(int userId)
        {
            var user = man.FindById(userId) ??
                throw new NotFoundException();
            return new(user)
            {
                MaxRole = GetMaxRole(user)
            };
        }

        public void Register(string userName, string email, string password)
        {
            try
            {
                man.Create(userName, email, password);
            }
            catch (Identity.IdentityException e)
            {
                throw new IdentityValidationException(e);
            }
        }

        public void SetBanned(int userId, bool banned, int callerId)
        {
            var user = man.FindById(userId) ??
                throw new NotFoundException();
            CheckRight(user, callerId);
            if (user.IsBanned != banned)
            {
                user.IsBanned = banned;
                man.Update(user);
            }
        }

        public void SetRole(int userId, string roleName, bool setHasRole, int callerId)
        {
            if (roleName.ToLower() == "user")
            {
                throw new InvalidOperationException();
            }

            if (Get(callerId).MaxRole <= RoleEnumConverter.GetRoleByName(roleName))
            {
                throw new AccessDeniedException("Cant manage higher or equal roles");
            }

            var user = man.FindById(userId) ??
                throw new NotFoundException();

            CheckRight(user, callerId);
            try
            {
                if (setHasRole)
                {
                    man.AddToRole(user, roleName);
                }
                else
                {
                    man.RemoveFromRole(user, roleName);
                }
            }
            catch (Identity.IdentityException) { }
        }

        public void SignIn(string userName, string password, bool remember)
        {
            if (!man.TrySignIn(userName, password, remember))
            {
                throw new AccessDeniedException("Wrong user name or password");
            }
        }

        public void SignOut()
        {
            man.SignOut();
        }

        public bool IsEmailUsed(string email)
        {
            return man.IsEmailUsed(email);
        }

        public bool IsUserNameUsed(string userName)
        {
            return man.IsUserNameUsed(userName);
        }
    }
}

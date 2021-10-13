using ForumTask.BLL.DTO;
using ForumTask.BLL.Interfaces;
using ForumTask.BLL.Exceptions;
using ForumTask.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumTask.BLL.Services {
    class UserService:IUserService {
        private readonly IIdentityManager man;

        public UserService(IIdentityManager man) {
            this.man = man;
        }

        private RoleEnum GetMaxRole(DAL.Entities.User user) {
            var roles = man.GetRoles(user);
            return roles.Select(s => RoleEnumConverter.GetRoleByName(s)).Max();
        }

        private void CheckRight(DAL.Entities.User victim, uint callerId) {
            var caller = Get(callerId);
            if (caller.MaxRole <= GetMaxRole(victim))
                throw new AccessDeniedException("To edit/delete this user your right-level must be greater then of that user");
        }

        public void Delete(uint userId, uint callerId) {
            var user = man.FindById(userId) ??
                 throw new NotFoundException();
            CheckRight(user, callerId);
            man.Delete(user);
        }

        public UserDTO Get(uint userId) {
            var user = man.FindById(userId) ??
                throw new NotFoundException();
            return new(user) {
                MaxRole = GetMaxRole(user)
            };
        }

        public void Register(string userName, string email, string password) {
            try {
                man.Create(userName, email, password);
            }catch(Identity.IdentityException e) {
                throw new IdentityValidationException(e);
            }
        }

        public void SetBanned(uint userId, bool banned, uint callerId) {
            var user = man.FindById(userId) ??
                throw new NotFoundException();
            CheckRight(user, callerId);
            if (user.IsBanned != banned) {
                user.IsBanned = banned;
                man.Update(user);
            }
        }

        public void SetRole(uint userId, string roleName, bool setHasRole, uint callerId) {
            var user = man.FindById(userId) ??
                throw new NotFoundException();
            CheckRight(user, callerId);
            try {
                if (setHasRole)
                    man.AddToRole(user, roleName);
                else
                    man.RemoveFromRole(user, roleName);
            } catch (Identity.IdentityException) { }
        }

        public void SignIn(string userName, string password, bool remember) {
            if (!man.TrySignIn(userName, password, remember))
                throw new AccessDeniedException("Wrong user name or password");
        }

        public void SignOut()
            => man.SignOut();

        public bool IsEmailUsed(string email)
            => man.IsEmailUsed(email);

        public bool IsUserNameUsed(string userName)
            => man.IsUserNameUsed(userName);
    }
}

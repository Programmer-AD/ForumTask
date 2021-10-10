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
        private readonly IUnitOfWork uow;

        public UserService(IUnitOfWork uow) {
            this.uow = uow;
        }

        private RoleEnum GetMaxRole(DAL.Entities.User user) {
            var roles = uow.IdentityManager.GetRoles(user);
            return roles.Select(s => RoleEnumConverter.GetRoleByName(s)).Max();
        }

        private void CheckRight(DAL.Entities.User victim, uint callerId) {
            var caller = Get(callerId);
            if (caller.MaxRole <= GetMaxRole(victim))
                throw new AccessDeniedException("To edit/delete this user your right-level must be greater then of that user");
        }

        public void Delete(uint userId, uint callerId) {
            var user = uow.IdentityManager.FindById(userId) ??
                 throw new NotFoundException();
            CheckRight(user, callerId);
            uow.IdentityManager.Delete(user);
        }

        public UserDTO Get(uint userId) {
            var user = uow.IdentityManager.FindById(userId) ??
                throw new NotFoundException();
            return new(user) {
                MaxRole = GetMaxRole(user)
            };
        }

        public void Register(string userName, string email, string password) {
            try {
                uow.IdentityManager.Create(userName, email, password);
            }catch(DAL.Identity.IdentityException e) {
                throw new IdentityValidationException(e);
            }
        }

        public void SetBanned(uint userId, bool banned, uint callerId) {
            var user = uow.IdentityManager.FindById(userId) ??
                throw new NotFoundException();
            CheckRight(user, callerId);
            if (user.IsBanned != banned) {
                user.IsBanned = banned;
                uow.IdentityManager.Update(user);
            }
        }

        public void SetRole(uint userId, string roleName, bool setHasRole, uint callerId) {
            var user = uow.IdentityManager.FindById(userId) ??
                throw new NotFoundException();
            CheckRight(user, callerId);
            try {
                if (setHasRole)
                    uow.IdentityManager.AddToRole(user, roleName);
                else
                    uow.IdentityManager.RemoveFromRole(user, roleName);
            } catch (DAL.Identity.IdentityException) { }
        }

        public void SignIn(string userName, string password, bool remember) {
            if (!uow.IdentityManager.TrySignIn(userName, password, remember))
                throw new AccessDeniedException("Wrong user name or password");
        }

        public void SignOut()
            => uow.IdentityManager.SignOut();
    }
}

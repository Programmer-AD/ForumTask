using System;
using ForumTask.BLL.DTO;
using ForumTask.BLL.Exceptions;
using ForumTask.BLL.Services;
using ForumTask.Tests.Fakes;
using NUnit.Framework;

namespace ForumTask.Tests.BllTests
{
    public class UserServiceTests
    {
        #region Init funcs
        private static FakeIdentityManager GetIdentityManager()
        {
            return new(
                        new()
                        {
                            User = new()
                            {
                                Id = 1,
                                IsBanned = false,
                            },
                            Roles = new() { RoleEnum.User }
                        }, new()
                        {
                            User = new()
                            {
                                Id = 2,
                                IsBanned = false,
                            },
                            Roles = new() { RoleEnum.User, RoleEnum.Moderator }
                        }, new()
                        {
                            User = new()
                            {
                                Id = 3,
                                IsBanned = false,
                            },
                            Roles = new() { RoleEnum.Admin }
                        });
        }

        private static UserService GetService(FakeIdentityManager man = null)
        {
            man ??= GetIdentityManager();

            return new UserService(man);
        }
        #endregion
        #region UserService.SetRole
        [Test]
        public void UserService_SetRole_RoleAttached()
        {
            var man = GetIdentityManager();
            var serv = GetService(man);
            int uid = 1, cid = 3;

            serv.SetRoleAsync(uid, "Moderator", true, cid);

            Assert.AreEqual(2, man.GetRoles(new() { Id = uid }).Count);
        }
        [Test]
        public void UserService_SetRole_RoleDetached()
        {
            var man = GetIdentityManager();
            var serv = GetService(man);
            int uid = 2, cid = 3;

            serv.SetRoleAsync(uid, "Moderator", false, cid);

            Assert.AreEqual(1, man.GetRoles(new() { Id = uid }).Count);
        }
        [Test]
        public void UserService_SetRole_RoleAttachedWhenHas()
        {
            var man = GetIdentityManager();
            var serv = GetService(man);
            int uid = 2, cid = 3;

            serv.SetRoleAsync(uid, "Moderator", true, cid);

            Assert.AreEqual(2, man.GetRoles(new() { Id = uid }).Count);
        }
        [Test]
        public void UserService_SetRole_RoleDetachedWhenHasnt()
        {
            var man = GetIdentityManager();
            var serv = GetService(man);
            int uid = 1, cid = 3;

            serv.SetRoleAsync(uid, "Moderator", false, cid);

            Assert.AreEqual(1, man.GetRoles(new() { Id = uid }).Count);
        }
        [Test]
        public void UserService_SetRole_ThrowInvalidOperationWhenTryToAttachRoleUser()
        {
            var man = GetIdentityManager();
            var serv = GetService(man);
            int uid = 1, cid = 3;

            Assert.Throws<InvalidOperationException>(() => serv.SetRoleAsync(uid, "User", true, cid));
        }
        [Test]
        public void UserService_SetRole_ThrowInvalidOperationWhenTryToDettachRoleUser()
        {
            var man = GetIdentityManager();
            var serv = GetService(man);
            int uid = 1, cid = 3;

            Assert.Throws<InvalidOperationException>(() => serv.SetRoleAsync(uid, "User", false, cid));
        }
        [Test]
        public void UserService_SetRole_ThrowNotFoundWhenUserNotFound()
        {
            var man = GetIdentityManager();
            var serv = GetService(man);
            int uid = 100, cid = 3;

            Assert.Throws<NotFoundException>(() => serv.SetRoleAsync(uid, "Moderator", true, cid));
        }
        [Test]
        public void UserService_SetRole_ThrowAccessDeniedWhenCallerModerator()
        {
            var man = GetIdentityManager();
            var serv = GetService(man);
            int uid = 1, cid = 2;

            Assert.Throws<AccessDeniedException>(() => serv.SetRoleAsync(uid, "Moderator", true, cid));
        }
        [Test]
        public void UserService_SetRole_ThrowAccessDeniedWhenCallerUser()
        {
            var man = GetIdentityManager();
            var serv = GetService(man);
            int uid = 1, cid = 1;

            Assert.Throws<AccessDeniedException>(() => serv.SetRoleAsync(uid, "Moderator", true, cid));
        }
        [Test]
        public void UserService_SetRole_ThrowAccessDeniedWhenTryToSetGreatterOrOwnRole()
        {
            var man = GetIdentityManager();
            var serv = GetService(man);
            int uid = 1, cid = 3;

            Assert.Throws<AccessDeniedException>(() => serv.SetRoleAsync(uid, "Admin", true, cid));
        }
        #endregion
    }
}

using System;
using ForumTask.BLL.DTO;
using ForumTask.BLL.Exceptions;
using ForumTask.BLL.Interfaces;
using ForumTask.BLL.Services;
using ForumTask.Tests.Fakes;
using ForumTask.Tests.Fakes.Repositories;
using NUnit.Framework;

namespace ForumTask.Tests.BllTests
{
    public class MessageServiceTests
    {
        #region Init funcs
        private static FakeUnitOfWork GetUow()
        {
            var uow = new FakeUnitOfWork();

            uow.SetRepository(new FakeMessageRepository(
                new()
                {
                    Id = 1,
                    AuthorId = 1,
                    TopicId = 1,
                    WriteTime = DateTime.UtcNow
                }, new()
                {
                    Id = 2,
                    AuthorId = 1,
                    TopicId = 1,
                    WriteTime = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(IMessageService.EditOrDeleteTime + 1))
                }, new()
                {
                    Id = 3,
                    AuthorId = 2,
                    TopicId = 1,
                    WriteTime = DateTime.UtcNow
                }, new()
                {
                    Id = 4,
                    AuthorId = 3,
                    TopicId = 2,
                    WriteTime = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(IMessageService.EditOrDeleteTime + 1))
                }, new()
                {
                    Id = 5,
                    AuthorId = 5,
                    TopicId = 2,
                    WriteTime = DateTime.UtcNow
                }, new()
                {
                    Id = 6,
                    AuthorId = null,
                    TopicId = 3,
                    WriteTime = DateTime.UtcNow
                }));

            return uow;
        }
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
                                IsBanned = true,
                            },
                            Roles = new() { RoleEnum.User }
                        }, new()
                        {
                            User = new()
                            {
                                Id = 3,
                                IsBanned = false,
                            },
                            Roles = new() { RoleEnum.User, RoleEnum.Moderator }
                        }, new()
                        {
                            User = new()
                            {
                                Id = 4,
                                IsBanned = false,
                            },
                            Roles = new() { RoleEnum.User, RoleEnum.Moderator }
                        }, new()
                        {
                            User = new()
                            {
                                Id = 5,
                                IsBanned = false,
                            },
                            Roles = new() { RoleEnum.Admin }
                        });
        }

        private static MessageService GetService(FakeUnitOfWork uow = null, FakeIdentityManager man = null)
        {
            uow ??= GetUow();
            man ??= GetIdentityManager();

            return new(uow, new UserService(man), new MarkService(uow));
        }
        #endregion
        #region MessageService.Delete
        [Test]
        public void MessageService_Delete_UserCanDeleteOwnWhileTimeNotExceedAndNotBanned()
        {
            var uow = GetUow();
            var serv = GetService(uow);
            long mesId = 1;
            int uid = 1;

            serv.Delete(mesId, uid);

            Assert.IsNull(uow.Messages.Get(mesId));
        }
        [Test]
        public void MessageService_Delete_ThrowAccessDenniedWhenTimeLimitExceed()
        {
            var uow = GetUow();
            var serv = GetService(uow);
            long mesId = 2;
            int uid = 1;

            Assert.Throws<AccessDeniedException>(() => serv.Delete(mesId, uid));
        }
        [Test]
        public void MessageService_Delete_ThrowAccessDenniedWhenUserTryDeleteOthers()
        {
            var uow = GetUow();
            var serv = GetService(uow);
            long mesId = 3;
            int uid = 1;

            Assert.Throws<AccessDeniedException>(() => serv.Delete(mesId, uid));
        }
        [Test]
        public void MessageService_Delete_ThrowAccessDenniedWhenUserBanned()
        {
            var uow = GetUow();
            var serv = GetService(uow);
            long mesId = 3;
            int uid = 2;

            Assert.Throws<AccessDeniedException>(() => serv.Delete(mesId, uid));
        }
        [TestCase(1, 3)]
        [TestCase(2, 3)]
        public void MessageService_Delete_ModeratorCanDeleteUserMessage(long mesId, int uid)
        {
            var uow = GetUow();
            var serv = GetService(uow);

            serv.Delete(mesId, uid);

            Assert.IsNull(uow.Messages.Get(mesId));
        }
        [Test]
        public void MessageService_Delete_ModeratorCanDeleteOwnWhenTimeLimitExceed()
        {
            var uow = GetUow();
            var serv = GetService(uow);
            long mesId = 4;
            int uid = 3;

            serv.Delete(mesId, uid);

            Assert.IsNull(uow.Messages.Get(mesId));
        }
        [Test]
        public void MessageService_Delete_ModeratorCanDeleteMessageOfDeletedUsers()
        {
            var uow = GetUow();
            var serv = GetService(uow);
            long mesId = 6;
            int uid = 3;

            serv.Delete(mesId, uid);

            Assert.IsNull(uow.Messages.Get(mesId));
        }
        [TestCase(4, 4)]
        [TestCase(5, 3)]
        public void MessageService_Delete_ThrowAccessDenniedWhenModeratorDeleteOtherModeratorOrAdminMessage(long mesId, int uid)
        {
            var uow = GetUow();
            var serv = GetService(uow);

            Assert.Throws<AccessDeniedException>(() => serv.Delete(mesId, uid));
        }
        [Test]
        public void MessageService_Delete_AdminCanDeleteModeratorOrUsers([Range(1, 5)] long mesId)
        {
            var uow = GetUow();
            var serv = GetService(uow);
            int uid = 5;

            serv.Delete(mesId, uid);

            Assert.IsNull(uow.Messages.Get(mesId));
        }
        [Test]
        public void MessageService_Delete_ThrowNotFoundWhenTryDeleteNotExist([Range(1, 5)] int uid)
        {
            var uow = GetUow();
            var serv = GetService(uow);
            long mesId = 100;

            Assert.Throws<NotFoundException>(() => serv.Delete(mesId, uid));
        }
        #endregion
        #region MessageService.GetMessageCount
        [TestCase(1, 3)]
        [TestCase(2, 2)]
        [TestCase(3, 1)]
        public void MessageService_GetMessageCount_CountsCorrectly(long topicId, int expected)
        {
            var serv = GetService();

            int result = serv.GetMessageCount(topicId);

            Assert.AreEqual(expected, result);
        }
        [Test]
        public void MessageService_GetMessageCount_Returns0IfNotFound()
        {
            var serv = GetService();

            int result = serv.GetMessageCount(100);

            Assert.Zero(result);
        }
        #endregion
    }
}

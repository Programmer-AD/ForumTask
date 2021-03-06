using ForumTask.BLL.DTO;
using ForumTask.BLL.Exceptions;
using ForumTask.BLL.Services;
using ForumTask.Tests.Fakes;
using NUnit.Framework;

namespace ForumTask.Tests.BllTests {
    public class TopicServicesTests {
        #region Init funcs
        private static FakeUnitOfWork GetUow() {
            var uow = new FakeUnitOfWork();

            return uow;
        }
        private static FakeIdentityManager GetIdentityManager()
            => new(
                new() {
                    User = new() {
                        Id = 1,
                        IsBanned = false,
                    },
                    Roles = new() { RoleEnum.User }
                }, new() {
                    User = new() {
                        Id = 2,
                        IsBanned = true,
                    },
                    Roles = new() { RoleEnum.User }
                });
        private static TopicService GetService(FakeUnitOfWork uow = null, FakeIdentityManager man = null) {
            uow ??= GetUow();
            man ??= GetIdentityManager();

            var userv = new UserService(man);

            return new(uow, new MessageService(uow, userv, new MarkService(uow)), userv);
        }
        #endregion
        #region TopicService.Create
        [Test]
        public void TopicService_Create_TopicWithMessageCreatesAndIdReturned() {
            var uow = new FakeUnitOfWork();
            var serv = GetService(uow);

            var id = serv.Create("Title", "Message", 1);

            Assert.IsNotNull(uow.Topics.Get(1L));
            Assert.IsNotNull(uow.Messages.Get(1L));
        }
        [Test]
        public void TopicService_Create_TopicWithoutMessageCreatesWhenMessageIsNull() {
            var uow = new FakeUnitOfWork();
            var serv = GetService(uow);

            var id = serv.Create("Title", null, 1);

            Assert.IsNotNull(uow.Topics.Get(1L));
            Assert.IsNull(uow.Messages.Get(1L));
        }
        [Test]
        public void TopicService_Create_TopicWithoutMessageCreatesWhenMessageIsEmpty() {
            var uow = new FakeUnitOfWork();
            var serv = GetService(uow);

            var id = serv.Create("Title", "", 1);

            Assert.IsNotNull(uow.Topics.Get(1L));
            Assert.IsNull(uow.Messages.Get(1L));
        }
        [Test]
        public void TopicService_Create_ThrowAccessDeniedIfUserBanned() {
            var uow = new FakeUnitOfWork();
            var serv = GetService(uow);

            Assert.Throws<AccessDeniedException>(() => serv.Create("Title", "Message", 2));
        }
        #endregion
    }
}

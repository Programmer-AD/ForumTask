using System.Linq.Expressions;
using AutoMapper;
using ForumTask.BLL.DTO;
using ForumTask.BLL.Exceptions;
using ForumTask.BLL.Interfaces;
using ForumTask.BLL.Services;
using ForumTask.DAL.Entities;
using ForumTask.DAL.Interfaces;
using Moq;
using NUnit.Framework;

namespace ForumTask.BLL.Tests.Services
{
    [TestFixture]
    public class MessageServiceTests
    {
        private const long messageId = 1;
        private const long callerId = 10;
        private const long authorId = 1;
        private const string newMessageText = "New text";

        private Mock<IRepository<Message>> messageRepositoryMock;
        private Mock<IRepository<Topic>> topicRepositoryMock;
        private Mock<IRepository<Mark>> markRepositoryMock;
        private Mock<IUserService> userServiceMock;
        private Mock<IMapper> mapperMock;

        private MessageService messageService;

        [SetUp]
        public void SetUp()
        {
            messageRepositoryMock = new();
            topicRepositoryMock = new();
            markRepositoryMock = new();
            userServiceMock = new();
            mapperMock = new();

            messageService = new(
                messageRepositoryMock.Object,
                topicRepositoryMock.Object,
                markRepositoryMock.Object,
                userServiceMock.Object,
                mapperMock.Object);
        }

        [Test]
        public void EditAsync_WhenMessageNotFound_ThrowNotFoundException()
        {
            SetExistingMessage(null);


            Task actAsync() => messageService.EditAsync(messageId, newMessageText, callerId);


            Assert.ThrowsAsync<NotFoundException>(actAsync);
        }

        [Test]
        public void EditAsync_WhenCallerIsBanned_ThrowAccessDeniedException()
        {
            var message = MakeMessage(MessageAuthorMode.Own, editTimeLimitExceed: false);

            SetExistingMessage(message);
            SetCallerInfo(RoleEnum.Admin, isBanned: true);


            Task actAsync() => messageService.EditAsync(messageId, newMessageText, callerId);


            Assert.ThrowsAsync<AccessDeniedException>(actAsync);
        }

        [Test]
        public void EditAsync_WhenUserIsRegularAndNotOwnMessage_ThrowAccessDeniedException()
        {
            var message = MakeMessage(MessageAuthorMode.NotOwn, editTimeLimitExceed: false);
            SetExistingMessage(message);
            SetCallerInfo(RoleEnum.User, isBanned: false);
            SetAuthorMaxRole(RoleEnum.User);


            Task actAsync() => messageService.EditAsync(messageId, newMessageText, callerId);


            Assert.ThrowsAsync<AccessDeniedException>(actAsync);
        }

        [Test]
        public void EditAsync_WhenUserIsRegularAndEditTimeLimitExceed_ThrowAccessDeniedException()
        {
            var message = MakeMessage(MessageAuthorMode.Own, editTimeLimitExceed: true);
            SetExistingMessage(message);
            SetCallerInfo(RoleEnum.User, isBanned: false);


            Task actAsync() => messageService.EditAsync(messageId, newMessageText, callerId);


            Assert.ThrowsAsync<AccessDeniedException>(actAsync);
        }

        [TestCase(RoleEnum.Moderator, RoleEnum.Moderator)]
        [TestCase(RoleEnum.Moderator, RoleEnum.Admin)]
        [TestCase(RoleEnum.Admin, RoleEnum.Admin)]
        public void EditAsync_WhenNotOwnMessageAndAuthorMaxRoleIsSameOrGreater_ThrowAccessDeniedException(RoleEnum callerRole, RoleEnum authorRole)
        {
            var message = MakeMessage(MessageAuthorMode.NotOwn, editTimeLimitExceed: false);
            SetExistingMessage(message);
            SetCallerInfo(callerRole, isBanned: false);
            SetAuthorMaxRole(authorRole);


            Task actAsync() => messageService.EditAsync(messageId, newMessageText, callerId);


            Assert.ThrowsAsync<AccessDeniedException>(actAsync);
        }

        [Test]
        public async Task EditAsync_WhenOwnMessageAndTimeLimitNotExceed_UpdateMessage()
        {
            var message = MakeMessage(MessageAuthorMode.Own, editTimeLimitExceed: false);
            SetExistingMessage(message);
            SetCallerInfo(RoleEnum.User, isBanned: false);


            await messageService.EditAsync(messageId, newMessageText, callerId);


            AssertMessageEdited(message);
        }

        [TestCase(RoleEnum.Moderator, RoleEnum.User)]
        [TestCase(RoleEnum.Admin, RoleEnum.Moderator)]
        public async Task EditAsync_WhenNotRegularUserAndHasGreaterRoleThanAuthor_UpdateMessage(RoleEnum callerRole, RoleEnum authorRole)
        {
            var message = MakeMessage(MessageAuthorMode.NotOwn, editTimeLimitExceed: false);
            SetExistingMessage(message);
            SetCallerInfo(callerRole, isBanned: false);
            SetAuthorMaxRole(authorRole);


            await messageService.EditAsync(messageId, newMessageText, callerId);


            AssertMessageEdited(message);
        }

        [Test]
        public async Task EditAsync_WhenNotRegularUserAndNoAuthor_UpdateMessage()
        {
            var message = MakeMessage(MessageAuthorMode.NoAuthor, editTimeLimitExceed: false);
            SetExistingMessage(message);
            SetCallerInfo(RoleEnum.Moderator, isBanned: false);


            await messageService.EditAsync(messageId, newMessageText, callerId);


            AssertMessageEdited(message);
        }

        [Test]
        public async Task EditAsync_WhenNotRegularUserAndTimeLimitExceed_UpdateMessage([Values] MessageAuthorMode authorMode)
        {
            var message = MakeMessage(authorMode, editTimeLimitExceed: true);
            SetExistingMessage(message);
            SetCallerInfo(RoleEnum.Moderator, isBanned: false);
            SetAuthorMaxRole(RoleEnum.User);


            await messageService.EditAsync(messageId, newMessageText, callerId);


            AssertMessageEdited(message);
        }

        private void SetExistingMessage(Message exsitingMessage)
        {
            messageRepositoryMock
                .Setup(x => x.GetAsync(It.IsAny<Expression<Func<Message, bool>>>()))
                .ReturnsAsync(exsitingMessage);
        }

        private void SetCallerInfo(RoleEnum maxRole, bool isBanned)
        {
            var caller = new UserDto()
            {
                MaxRole = maxRole,
                IsBanned = isBanned
            };

            userServiceMock
                .Setup(x => x.GetAsync(callerId))
                .ReturnsAsync(caller);
        }

        private void SetAuthorMaxRole(RoleEnum maxRole)
        {
            var author = new UserDto()
            {
                MaxRole = maxRole,
            };

            userServiceMock
                .Setup(x => x.GetAsync(authorId))
                .ReturnsAsync(author);
        }

        private static Message MakeMessage(MessageAuthorMode messageAuthorMode, bool editTimeLimitExceed)
        {
            long? author = messageAuthorMode switch
            {
                MessageAuthorMode.NoAuthor => null,
                MessageAuthorMode.Own => callerId,
                MessageAuthorMode.NotOwn => authorId,
                _ => throw new NotImplementedException(),
            };

            var message = new Message()
            {
                AuthorId = author,
                WriteTime = GetWriteTime(editTimeLimitExceed),
            };

            return message;
        }

        private static DateTime GetWriteTime(bool editTimeLimitExceed)
        {
            var time = DateTime.UtcNow;

            if (editTimeLimitExceed)
            {
                time -= TimeSpan.FromMinutes(IMessageService.EditOrDeleteTime);
            }

            return time;
        }
        private void AssertMessageEdited(Message message)
        {
            Assert.That(message.Text, Is.EqualTo(newMessageText));
            messageRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Message>()), Times.Once);
        }

        public enum MessageAuthorMode
        {
            NoAuthor,
            Own,
            NotOwn
        }
    }
}

using AutoMapper;
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
        public async Task EditAsync_WhenMessageNotFound_ThrowNotFoundException()
        {
            //TODO: EditAsync tests
        }

        [Test]
        public async Task EditAsync_WhenCallerIsBanned_ThrowAccessDeniedException()
        {

        }

        [Test]
        public async Task EditAsync_WhenUserIsRegularAndNotOwnMessage_ThrowAccessDeniedException()
        {

        }

        [Test]
        public async Task EditAsync_WhenUserIsRegularAndEditTimeLimitExceed_ThrowAccessDeniedException()
        {

        }

        [Test]
        public async Task EditAsync_WhenNotOwnMessageAndAuthorMaxRoleIsSameOrGreater_ThrowAccessDeniedException()
        {

        }

        [Test]
        public async Task EditAsync_WhenOwnMessageAndTimeLimitNotExceed_UpdateMessage()
        {

        }

        [Test]
        public async Task EditAsync_WhenNotRegularUserAndHasGreaterRoleThanAuthor_UpdateMessage()
        {

        }
    }
}

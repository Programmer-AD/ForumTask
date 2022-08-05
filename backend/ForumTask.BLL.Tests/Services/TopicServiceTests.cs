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
    public class TopicServiceTests
    {
        private const long callerId = 10;
        private const string topicTitle = "Topic title";
        private const string messageText = "Message text";
        private const string emptyMessage = "";

        private Mock<IRepository<Message>> messageRepositoryMock;
        private Mock<IRepository<Topic>> topicRepositoryMock;
        private Mock<IUserService> userServiceMock;
        private Mock<IMapper> mapperMock;

        private TopicService topicService;

        [SetUp]
        public void SetUp()
        {
            messageRepositoryMock = new();
            topicRepositoryMock = new();
            userServiceMock = new();
            mapperMock = new();

            topicService = new(
                topicRepositoryMock.Object,
                messageRepositoryMock.Object,
                userServiceMock.Object,
                mapperMock.Object);
        }

        [Test]
        public void CreateAsync_WhenCallerIsBanned_ThrowAccessDeniedException()
        {
            SetCallerBanned(true);


            Task actAsync() => topicService.CreateAsync(topicTitle, messageText, callerId);


            Assert.ThrowsAsync<AccessDeniedException>(actAsync);
        }

        [Test]
        public async Task CreateAsync_WhenCallerIsNotBanned_CreateTopic()
        {
            SetCallerBanned(false);


            await topicService.CreateAsync(topicTitle, emptyMessage, callerId);


            topicRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<Topic>()), Times.Once);
        }

        [Test]
        public async Task CreateAsync_WhenTopicCreatedAndMessageTextIsNotNullOrEmpty_CreateMessage()
        {
            SetCallerBanned(false);


            await topicService.CreateAsync(topicTitle, messageText, callerId);


            messageRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<Message>()), Times.Once);
        }

        [Test]
        public async Task CreateAsync_WhenTopicCreatedAndMessageTextIsEmpty_DontCreateMessage()
        {
            SetCallerBanned(false);


            await topicService.CreateAsync(topicTitle, emptyMessage, callerId);


            messageRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<Message>()), Times.Never);
        }

        [Test]
        public async Task CreateAsync_WhenTopicCreatedAndMessageTextIsNull_DontCreateMessage()
        {
            SetCallerBanned(false);


            await topicService.CreateAsync(topicTitle, messageText: null, callerId);


            messageRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<Message>()), Times.Never);
        }

        [Test]
        public async Task CreateAsync_WhenTopicCreatedWithoutMessage_ForceSaveChanges()
        {
            SetCallerBanned(false);


            await topicService.CreateAsync(topicTitle, emptyMessage, callerId);


            topicRepositoryMock.Verify(x => x.ForceSaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task CreateAsync_WhenTopicCreatedWithMessage_ForceSaveChanges()
        {
            SetCallerBanned(false);


            await topicService.CreateAsync(topicTitle, messageText, callerId);


            topicRepositoryMock.Verify(x => x.ForceSaveChangesAsync(), Times.Once);
        }

        private void SetCallerBanned(bool isBanned)
        {
            var caller = new UserDto()
            {
                IsBanned = isBanned
            };

            userServiceMock
                .Setup(x => x.GetAsync(callerId))
                .ReturnsAsync(caller);
        }
    }
}

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
    public class TopicServiceTests
    {
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
        public async Task CreateAsync_WhenCallerIsBanned_ThrowAccessDeniedException()
        {
            //TODO: CreateAsync tests
        }

        [Test]
        public async Task CreateAsync_WhenCallerIsNotBanned_CreateTopic()
        {

        }

        [Test]
        public async Task CreateAsync_WhenTopicCreatedAndMessageTextIsNotNullOrEmpty_CreateMessage()
        {

        }

        [Test]
        public async Task CreateAsync_WhenTopicCreatedAndMessageTextIsNullOrEmpty_DontCreateMessage()
        {

        }

        [Test]
        public async Task CreateAsync_WhenTopicCreated_ForceSaveChanges()
        {

        }

        [Test]
        public async Task CreateAsync_WhenTopicCreatedWithMessage_ForceSaveChanges()
        {

        }
    }
}

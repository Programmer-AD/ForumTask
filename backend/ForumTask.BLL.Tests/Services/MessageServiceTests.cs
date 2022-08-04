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

        //TODO: EditAsync
    }
}

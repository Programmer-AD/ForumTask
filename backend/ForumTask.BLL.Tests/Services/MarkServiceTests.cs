using AutoMapper;
using ForumTask.BLL.Services;
using ForumTask.DAL.Entities;
using ForumTask.DAL.Interfaces;
using Moq;
using NUnit.Framework;

namespace ForumTask.BLL.Tests.Services
{
    [TestFixture]
    public class MarkServiceTests
    {
        private Mock<IRepository<Mark>> markRepositoryMock;
        private Mock<IMapper> mapperMock;

        private MarkService markService;

        [SetUp]
        public void SetUp()
        {
            markRepositoryMock = new();
            mapperMock = new();

            markService = new(
                markRepositoryMock.Object,
                mapperMock.Object);
        }


    }
}

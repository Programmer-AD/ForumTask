using AutoMapper;
using ForumTask.BLL.Services;
using ForumTask.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;

namespace ForumTask.BLL.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<UserManager<User>> userManagerMock;
        private Mock<SignInManager<User>> signInManagerMock;
        private Mock<IMapper> mapperMock;

        private UserService userService;

        [SetUp]
        public void SetUp()
        {
            userManagerMock = new();
            signInManagerMock = new();
            mapperMock = new();

            userService = new(
                userManagerMock.Object,
                signInManagerMock.Object,
                mapperMock.Object);
        }

        //TODO: SetRoleAsync
    }
}

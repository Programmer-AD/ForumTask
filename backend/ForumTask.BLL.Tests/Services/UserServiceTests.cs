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


        [Test]
        public async Task SetRoleAsync_WhenRoleNameIsIncorrect_ThrowInvalidOpertionException()
        {
            //TODO: SetRoleAsync tests
        }

        [Test]
        public async Task SetRoleAsync_WhenRoleNameUser_ThrowInvalidOperationException()
        {

        }

        [Test]
        public async Task SetRoleAsync_WhenCallerRoleIsLessThanOrEqualToSettedRole_ThrowAccessDeniedException()
        {

        }

        [Test]
        public async Task SetRoleAsync_WhenTargetUserNotFound_ThrowNotFoundException()
        {

        }

        [Test]
        public async Task SetRoleAsync_WhenCallerMaxRoleIsLessThanOrEqualToTargetRole_ThrowAccessDeniedException()
        {

        }

        [Test]
        public async Task SetRoleAsync_WhenSetHasRoleIsTrue_AddToRole()
        {

        }

        [Test]
        public async Task SetRoleAsync_WhenSetHasRoleIsFalse_RemoveFromRole()
        {

        }

        [Test]
        public async Task SetRoleAsync_ParseOfRoleIsCaseInsensetive()
        {

        }
    }
}

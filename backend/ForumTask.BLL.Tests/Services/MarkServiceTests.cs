using System.Linq.Expressions;
using AutoMapper;
using ForumTask.BLL.DTO;
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
        private const sbyte zeroNewValue = 0;
        private const sbyte positiveNewValue = 1;

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

        [Test]
        public async Task SetAsync_WhenNewValueIs0AndMarkExists_DeleteMark()
        {
            var markDto = MakeMarkDto(zeroNewValue);
            SetMarkExists(true);


            await markService.SetAsync(markDto);


            markRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Mark>()), Times.Once);
        }

        [Test]
        public async Task SetAsync_WhenNewValueIs0ButMarkNotExist_DontDeleteMark()
        {
            var markDto = MakeMarkDto(zeroNewValue);
            SetMarkExists(false);


            await markService.SetAsync(markDto);


            markRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Mark>()), Times.Never);
        }

        [Test]
        public async Task SetAsync_WhenNewValueIsNot0AndMarkNotExist_CreateNewMark()
        {
            var markDto = MakeMarkDto(positiveNewValue);
            SetMarkExists(false);


            await markService.SetAsync(markDto);


            markRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<Mark>()), Times.Once);
        }

        [Test]
        public async Task SetAsync_WhenNewValueIsNot0AndExistingIsDifferent_UpdateMark()
        {
            var markDto = MakeMarkDto(positiveNewValue);
            var existingMark = MakeMark(MarkType.Negative);
            SetExistingMark(existingMark);


            await markService.SetAsync(markDto);


            markRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Mark>()), Times.Once);
        }

        [Test]
        public async Task SetAsync_WhenNewValueIsNot0ButExistingIsSame_DontUpdateMark()
        {
            var markDto = MakeMarkDto(positiveNewValue);
            var existingMark = MakeMark(MarkType.Positive);
            SetExistingMark(existingMark);


            await markService.SetAsync(markDto);


            markRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Mark>()), Times.Never);
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(sbyte.MaxValue)]
        public async Task SetAsync_TreatsAllPositiveValuesAsPositiveMark(sbyte newValue)
        {
            var markDto = MakeMarkDto(newValue);
            var existingMark = MakeMark(MarkType.Negative);
            SetExistingMark(existingMark);


            await markService.SetAsync(markDto);


            Assert.That(existingMark.Type, Is.EqualTo(MarkType.Positive));
        }

        [TestCase(-1)]
        [TestCase(-5)]
        [TestCase(sbyte.MinValue)]
        public async Task SetAsync_TreatsAllNegtiveValuesAsNegativeMark(sbyte newValue)
        {
            var markDto = MakeMarkDto(newValue);
            var existingMark = MakeMark(MarkType.Positive);
            SetExistingMark(existingMark);


            await markService.SetAsync(markDto);


            Assert.That(existingMark.Type, Is.EqualTo(MarkType.Negative));
        }

        private void SetMarkExists(bool exists)
        {
            var existingMark = exists ? new Mark() : null;

            SetExistingMark(existingMark);
        }

        private void SetExistingMark(Mark existingMark)
        {
            markRepositoryMock
                .Setup(x => x.GetAsync(It.IsAny<Expression<Func<Mark, bool>>>()))
                .ReturnsAsync(existingMark);
        }

        private static MarkDto MakeMarkDto(sbyte value)
        {
            var markDto = new MarkDto()
            {
                Value = value
            };

            return markDto;
        }

        private static Mark MakeMark(MarkType markType)
        {
            var mark = new Mark()
            {
                Type = markType
            };

            return mark;
        }
    }
}

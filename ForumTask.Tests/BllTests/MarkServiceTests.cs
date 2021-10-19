using ForumTask.BLL.Services;
using ForumTask.DAL.Entities;
using ForumTask.Tests.Fakes;
using ForumTask.Tests.Fakes.Repositories;
using NUnit.Framework;

namespace ForumTask.Tests.BllTests {
    public class MarkServiceTests {
        #region Init funcs
        private static FakeUnitOfWork GetUow() {
            var uow = new FakeUnitOfWork();
            uow.SetRepository(new FakeMessageRepository(
                new() { Id = 1 },
                new() { Id = 3 }));
            uow.SetRepository(new FakeMarkRepository(
                new() {
                    MessageId = 1,
                    UserId = 1,
                    Type = MarkType.Negative
                },
                new() {
                    MessageId = 1,
                    UserId = 2,
                    Type = MarkType.Positive
                },
                new() {
                    MessageId = 1,
                    UserId = 3,
                    Type = MarkType.Negative
                },
                new() {
                    MessageId = 2,
                    UserId = 1,
                    Type = MarkType.Positive
                },
                new() {
                    MessageId = 2,
                    UserId = 2,
                    Type = MarkType.Positive
                }));
            return uow;
        }
        private static MarkService GetService(FakeUnitOfWork uow = null)
            => new(uow ?? GetUow());
        #endregion
        #region Mark.Set
        [Test]
        public void Mark_Set_DeleteMarkIfValueIs0() {
            var uow = GetUow();
            var serv = GetService(uow);

            serv.Set(new() { UserId = 1, MessageId = 2, Value = 0 });

            Assert.IsNull(uow.Marks.Get(1, 2L));
        }
        [Test]
        public void Mark_Set_DontThrowExceptionIfTryToDeleteNotExisting() {
            var uow = GetUow();
            var serv = GetService(uow);

            Assert.DoesNotThrow(() => {
                serv.Set(new() { UserId = 100, MessageId = 100, Value = 0 });
            });
        }
        [Test]
        public void Mark_Set_ChangeMarkIfValueIsNot0() {
            var uow = GetUow();
            var serv = GetService(uow);

            serv.Set(new() { UserId = 1, MessageId = 2, Value = -1 });

            Assert.True(uow.Marks.Get(1, 2L).Type == MarkType.Negative);
        }
        [Test]
        public void Mark_Set_CreateMarkIfValueIsNot0() {
            var uow = GetUow();
            var serv = GetService(uow);

            serv.Set(new() { UserId = 100, MessageId = 100, Value = -1 });

            Assert.True(uow.Marks.Get(1, 2L).Type == MarkType.Positive);
        }
        #endregion
        #region Mark.GetOwn
        [TestCase(1, 1, -1)]
        [TestCase(1, 2, 1)]
        [TestCase(2, 1, 1)]
        public void Mark_GetOwn_ReturnsCorrectIfMarkExist(int uid, long mid, sbyte expected) {
            var serv = GetService();

            var result = serv.GetOwn(uid, mid);

            Assert.AreEqual(expected, result);
        }
        [TestCase(2, 3)]
        [TestCase(100, 100)]
        public void Mark_GetOwn_ReturnsCorrectIfMarkNotExist(int uid, long mid) {
            var serv = GetService();

            var result = serv.GetOwn(uid, mid);

            Assert.Zero(result);
        }
        #endregion
        //#region Mark.GetTotal
        //[TestCase(1, -1)]
        //[TestCase(2, 2)]
        //public void Mark_GetTotal_ReturnsCorrectIfMessageExist(long id, long expected) {
        //    var serv = GetService();

        //    var result = serv.GetTotal(id);

        //    Assert.AreEqual(expected, result);
        //}
        //[Test]
        //public void Mark_GetTotal_ReturnsCorrectIfMessageNotExist() {
        //    var serv = GetService();

        //    var result = serv.GetTotal(100);

        //    Assert.Zero(result);
        //}
        //#endregion
    }
}

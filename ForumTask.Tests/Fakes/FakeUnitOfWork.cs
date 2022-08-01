using ForumTask.DAL.Interfaces;
using ForumTask.Tests.Fakes.Repositories;

namespace ForumTask.Tests.Fakes
{
    internal class FakeUnitOfWork : IUnitOfWork
    {
        private ITopicRepository topic;
        public ITopicRepository Topics => topic ??= new FakeTopicRepository();

        private IMessageRepository message;
        public IMessageRepository Messages => message ??= new FakeMessageRepository();

        private IMarkRepository mark;
        public IMarkRepository Marks => mark ??= new FakeMarkRepository();

        public void SaveChanges()
        {
            ChangesSaved = true;
        }

        public bool ChangesSaved { get; private set; }
        public void ResetSave()
        {
            ChangesSaved = false;
        }

        public void SetRepository<T>(IRepository<T> rep)
        {
            switch (rep)
            {
                case ITopicRepository r:
                    topic = r;
                    break;
                case IMarkRepository r:
                    mark = r;
                    break;
                case IMessageRepository r:
                    message = r;
                    break;
            }
        }
    }
}

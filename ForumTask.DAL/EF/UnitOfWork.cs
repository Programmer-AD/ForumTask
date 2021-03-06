using System;
using ForumTask.DAL.EF.Repositories;
using ForumTask.DAL.Interfaces;

namespace ForumTask.DAL.EF {
    class UnitOfWork : IUnitOfWork, IDisposable {
        private ITopicRepository topic;
        public ITopicRepository Topics => topic ??= new TopicRepository(db);

        private IMessageRepository message;
        public IMessageRepository Messages => message ??= new MessageRepository(db);

        private IMarkRepository mark;
        public IMarkRepository Marks => mark ??= new MarkRepository(db);

        private readonly ForumContext db;

        public UnitOfWork(ForumContext context) {
            db = context;
        }

        public void SaveChanges()
            => db.SaveChanges();

        public void Dispose() {
            db.Dispose();
        }
    }
}

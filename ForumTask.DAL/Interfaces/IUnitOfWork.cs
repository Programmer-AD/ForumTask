namespace ForumTask.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        ITopicRepository Topics { get; }
        IMessageRepository Messages { get; }
        IMarkRepository Marks { get; }

        void SaveChanges();
    }
}

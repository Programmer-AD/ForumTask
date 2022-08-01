using ForumTask.DAL.Entities;

namespace ForumTask.DAL.Interfaces
{
    public interface IMarkRepository : IRepository<Mark>
    {
        /// <summary>
        /// Count of marks of <paramref name="type"/> attached to <paramref name="messageId"/>
        /// </summary>
        /// <param name="messageId">Id of message which mark is counted</param>
        /// <param name="type">Type of mark to count</param>
        /// <returns>Count of marks</returns>
        long GetCountOfType(long messageId, sbyte type);
    }
}

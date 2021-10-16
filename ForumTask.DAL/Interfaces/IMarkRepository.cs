using ForumTask.DAL.Entities;

namespace ForumTask.DAL.Interfaces {
    public interface IMarkRepository : IRepository<Mark> {
        /// <summary>
        /// Count total mark of message
        /// </summary>
        /// <param name="messageId">Id of message which mark is counted</param>
        /// <returns>Total mark of message</returns>
        long GetMarkValue(long messageId);
    }
}

using ForumTask.BLL.DTO;

namespace ForumTask.BLL.Interfaces {
    public interface IMarkService {
        /// <summary>
        /// <para>
        /// If <see cref="MarkDTO.Value"/> is 0, then deletes mark,
        /// else creates or updates it
        /// </para>
        /// </summary>
        /// <param name="mark">Mark to set</param>
        void Set(MarkDTO mark);
        /// <summary>
        /// Gets value of user mark
        /// </summary>
        /// <param name="userId">User which mark to get</param>
        /// <param name="messageId">Message from which mark to get</param>
        /// <returns>Value of mark or 0 if don`t exist</returns>
        sbyte GetOwn(int userId, long messageId);
        /// <summary>
        /// Gets total mark of message with <paramref name="messageId"/>
        /// <para>
        /// If there is no message with such id than 0 is returned
        /// </para>
        /// </summary>
        /// <param name="messageId">Id of message</param>
        /// <returns>Total mark of message</returns>
        long GetTotal(long messageId);
    }
}

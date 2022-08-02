using ForumTask.BLL.DTO;

namespace ForumTask.BLL.Interfaces
{
    public interface IMarkService
    {
        /// <summary>
        /// <para>
        /// If <see cref="MarkDto.Value"/> is 0, then deletes mark,
        /// else creates or updates it
        /// </para>
        /// </summary>
        /// <param name="mark">Mark to set</param>
        Task SetAsync(MarkDto mark);

        /// <summary>
        /// Gets value of user mark
        /// </summary>
        /// <param name="userId">User which mark to get</param>
        /// <param name="messageId">Message from which mark to get</param>
        /// <returns>Value of mark or 0 if don`t exist</returns>
        Task<sbyte> GetOwnAsync(long userId, long messageId);
    }
}

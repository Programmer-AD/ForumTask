using System.Collections.Generic;
using ForumTask.DAL.Entities;

namespace ForumTask.DAL.Interfaces {
    public interface IMessageRepository : IRepository<Message> {
        /// <summary>
        /// Picks messages attached to <paramref name="topicId"/> orders them by <see cref="Message.WriteTime"/> asc
        /// skips <paramref name="offset"/> and then takes <paramref name="count"/>
        /// </summary>
        /// <param name="topicId">Id of topic</param>
        /// <param name="count">Count of entities to return</param>
        /// <param name="offset">Number of entities to skip</param>
        /// <returns>Collection of messages</returns>
        IEnumerable<Message> GetTopOld(long topicId, int count, int offset);
        /// <summary>
        /// Gets count of messages attached to <paramref name="topicId"/>
        /// </summary>
        /// <param name="topicId">Id of topic which messages needs to count</param>
        /// <returns>Count of messages attached to topic with id=<paramref name="topicId"/> or 0 if topic not found</returns>
        int GetMessageCount(long topicId);
    }
}

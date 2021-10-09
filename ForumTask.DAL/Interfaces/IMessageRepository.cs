using ForumTask.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumTask.DAL.Interfaces {
    public interface IMessageRepository:IRepository<Message> {
        /// <summary>
        /// Picks messages attached to <paramref name="topic"/> orders them by <see cref="Message.WriteTime"/> asc
        /// skips <paramref name="offset"/> and then takes <paramref name="count"/>
        /// <para>
        /// If <paramref name="topic"/> is null, throws <see cref="ArgumentNullException"/>
        /// </para>
        /// </summary>
        /// <param name="count">Count of entities to return</param>
        /// <param name="offset">Number of entities to skip</param>
        /// <returns>Collection of messages</returns>
        /// <exception cref="ArgumentNullException"/>
        IEnumerable<Message> GetTopOld(Topic topic,int count, int offset);
    }
}

using ForumTask.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumTask.DAL.Interfaces {
    public interface ITopicRepository:IRepository<Topic> {
        /// <summary>
        /// Order topics by <see cref="Topic.CreateTime"/> desc then skips <paramref name="offset"/> and takes <paramref name="count"/> as result
        /// </summary>
        /// <param name="count">Count of entities to return</param>
        /// <param name="offset">Number of entities to skip</param>
        /// <returns>Ordered collection of topics</returns>
        IEnumerable<Topic> GetTopNew(int count, int offset);
        /// <summary>
        /// Pick topics which <see cref="Topic.Title"/> contains <paramref name="title"/> 
        /// then orders them by <see cref="Topic.CreateTime"/> desc,
        /// skips <paramref name="offset"/> and takes <paramref name="count"/> as result
        /// </summary>
        /// <param name="title">Title to search</param>
        /// <param name="count">Count of entities to return</param>
        /// <param name="offset">Number of entities to skip</param>
        /// <returns>Filtered and order collection of topics</returns>
        IEnumerable<Topic> FindNewestByTitle(string title, int count, int offset);
        /// <summary>
        /// Gets count of messages attached to <paramref name="topic"/>
        /// <para>
        /// If <paramref name="topic"/> is null, throws <see cref="ArgumentNullException"/>
        /// </para>
        /// </summary>
        /// <param name="topic"></param>
        /// <returns>Count of messages attached to <paramref name="topic"/></returns>
        /// <exception cref="ArgumentNullException"/>
        int GetMessageCount(Topic topic);
    }
}

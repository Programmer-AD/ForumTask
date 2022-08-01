using System.Collections.Generic;
using ForumTask.DAL.Entities;

namespace ForumTask.DAL.Interfaces
{
    public interface ITopicRepository : IRepository<Topic>
    {
        /// <summary>
        /// Order topics by <see cref="Topic.CreateTime"/> desc then skips <paramref name="offset"/> and takes <paramref name="count"/> as result
        /// <para>
        /// If <paramref name="searchTitle"/> is not empty or null previously filter topics by title 
        /// </para>
        /// </summary>
        /// <param name="count">Count of entities to return</param>
        /// <param name="offset">Number of entities to skip</param>
        /// <param name="searchTitle">Title to search</param>
        /// <returns>Ordered collection of topics</returns>
        IEnumerable<Topic> GetTopNew(int count, int offset, string searchTitle = "");
        /// <summary>
        /// Gets total count of topic
        /// </summary>
        /// <returns>Count of topics</returns>
        int Count();
    }
}

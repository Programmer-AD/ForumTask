using System.Collections.Generic;
using ForumTask.BLL.DTO;
using ForumTask.BLL.Exceptions;

namespace ForumTask.BLL.Interfaces
{
    public interface ITopicService
    {
        /// <summary>
        /// Count of topics that will be shown on page
        /// Must be >0
        /// </summary>
        public const int PageSize = 10;

        /// <summary>
        /// Time after creation in minutes in which Topic can be renamed/deleted by user
        /// </summary>
        /// <remarks>
        /// Edit and delete time are equal because 
        /// if they were different one action can use another for same result
        /// </remarks>
        public const double EditOrDeleteTime = 5;

        /// <summary>
        /// Creates new topic with title <paramref name="title"/>
        /// and attaches new message with text <paramref name="message"/>
        /// <para>
        /// If message is null or empty, message doesn`t create and attach
        /// </para>
        /// <para>
        /// If user can`t create new topic, throws <see cref="AccessDeniedException"/>
        /// </para>
        /// </summary>
        /// <param name="title">Title of topic</param>
        /// <param name="message">Text of message to attach or null</param>
        /// <param name="callerId">Id of user, who creates topic</param>
        /// <returns>Id of created topic</returns>
        /// <exception cref="AccessDeniedException"/>
        Task<long> CreateAsync(string title, string message, long callerId);

        /// <summary>
        /// Deletes topic
        /// <para>
        /// For a simple user there is a time limit for delete after creation in minutes {<see cref="EditOrDeleteTime"/>}
        /// </para>
        /// <para>
        /// If topic not found, throws <see cref="NotFoundException"/>
        /// </para>
        /// <para>
        /// If user can`t delete topic, throws <see cref="AccessDeniedException"/>
        /// </para>
        /// </summary>
        /// <param name="topicId">Topic to delete</param>
        /// <param name="callerId">Id of user who tries to delete topic</param>
        /// <exception cref="AccessDeniedException"/>
        /// <exception cref="NotFoundException"/>
        Task DeleteAsync(long topicId, long callerId);

        /// <summary>
        /// Edits title of topic
        /// <para>
        /// For a simple user there is a time limit for rename after creation in minutes {<see cref="EditOrDeleteTime"/>}
        /// </para>
        /// <para>
        /// If topic not found, throws <see cref="NotFoundException"/>
        /// </para>
        /// <para>
        /// If user can`t rename topic, throws <see cref="AccessDeniedException"/>
        /// </para>
        /// </summary>
        /// <param name="topicId">Topic to rename</param>
        /// <param name="newTitle">New title of topic</param>
        /// <param name="callerId">Id of user who tries to rename topic</param>
        /// <exception cref="AccessDeniedException"/>
        /// <exception cref="NotFoundException"/>
        Task RenameAsync(long topicId, string newTitle, long callerId);

        /// <summary>
        /// Gets topic by id
        /// <para>
        /// If topic not found, throws <see cref="NotFoundException"/>
        /// </para>
        /// </summary>
        /// <param name="id">Id of topic to get</param>
        /// <returns>Topic with id</returns>
        /// <exception cref="NotFoundException"/>
        Task<TopicDto> GetAsync(long id);

        /// <summary>
        /// Gets {<see cref="PageSize"/>} top new topics
        /// <para>
        /// If <paramref name="searchTitle"/> is not empty or null, 
        /// picks only that title contains <paramref name="searchTitle"/> (Case-Independent)
        /// </para>
        /// </summary>
        /// <param name="page">Number of page to get (zero-based)</param>
        /// <returns>Collection of topics</returns>
        Task<IEnumerable<TopicDto>> GetTopNewAsync(int page, string searchTitle = "");

        /// <summary>
        /// Gets count of pages to show all topics
        /// </summary>
        /// <returns>Count of pages</returns>
        Task<int> GetPagesCountAsync();
    }
}

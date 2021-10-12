using ForumTask.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumTask.BLL.Exceptions;

namespace ForumTask.BLL.Interfaces {
    public interface IMessageService {
        /// <summary>
        /// Count of messages that will be shown on page
        /// </summary>
        public const int PageSize = 20;
        /// <summary>
        /// Time after creation in minutes in which Message can be edited/deleted by user
        /// </summary>
        /// <remarks>
        /// Edit and delete time are equal because 
        /// if they were different one action can use another for same result
        /// </remarks>
        public const double EditOrDeleteTime = 5;
        /// <summary>
        /// Adds new message
        /// <para>
        /// If author can`t write messages, throw <see cref="AccessDeniedException"/>
        /// </para>
        /// <para>
        /// If no topic with such id, throw <see cref="NotFoundException"/>
        /// </para>
        /// </summary>
        /// <param name="message">Message to add</param>
        /// <exception cref="AccessDeniedException"/>
        /// <exception cref="NotFoundException"/>
        void Add(MessageDTO message);
        /// <summary>
        /// Edits message text
        /// <para>
        /// For a simple user there is a time limit for edit after creation in minutes {<see cref="EditOrDeleteTime"/>}
        /// </para>
        /// <para>
        /// If message not found, throws <see cref="NotFoundException"/>
        /// </para>
        /// <para>
        /// If user can`t edit message, throws <see cref="AccessDeniedException"/>
        /// </para>
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="newText"></param>
        /// <param name="userId"></param>
        /// <exception cref="NotFoundException"/>
        /// <exception cref="AccessDeniedException"/>
        void Edit(ulong messageId, string newText, uint userId);
        /// <summary>
        /// Deletes message
        /// <para>
        /// For a simple user there is a time limit for delete after creation in minutes {<see cref="EditOrDeleteTime"/>}
        /// </para>
        /// <para>
        /// If message not found, throws <see cref="NotFoundException"/>
        /// </para>
        /// <para>
        /// If user can`t delete message, throws <see cref="AccessDeniedException"/>
        /// </para>
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="userId"></param>
        /// <exception cref="NotFoundException"/>
        /// <exception cref="AccessDeniedException"/>
        void Delete(ulong messageId, uint userId);
        /// <summary>
        /// Gets {<see cref="PageSize"/>} messages in topic ordered by write-time ascending (top old)
        /// <para>
        /// If topic not found, result collection size will be 0
        /// </para>
        /// </summary>
        /// <param name="topicId">Id of topic from which get messages</param>
        /// <param name="page">Number of page to get (zero-based)</param>
        /// <returns>Collection of messages</returns>
        IEnumerable<MessageDTO> GetTopOld(ulong topicId, uint page);
        /// <summary>
        /// Gets count of messages attached to topic
        /// <para>
        /// If topic not found, returns 0
        /// </para>
        /// </summary>
        /// <param name="topicId">Id of topic which messages are counted</param>
        /// <returns>Count of messages or 0 topic not found</returns>
        int GetMessageCount(ulong topicId);
        /// <summary>
        /// Creates message and attaches it to newly created topic
        /// <para>
        /// Function for only internal purposes
        /// </para>
        /// </summary>
        /// <param name="message">Message to add</param>
        /// <param name="topic">Topic to which message wil be attached</param>
        internal void Add(MessageDTO message,DAL.Entities.Topic topic);
    }
}

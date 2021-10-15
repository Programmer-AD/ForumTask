using ForumTask.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumTask.DAL.Interfaces {
    public interface IMarkRepository:IRepository<Mark> {
        /// <summary>
        /// Count total mark of message
        /// </summary>
        /// <param name="messageId">Id of message which mark is counted</param>
        /// <returns>Total mark of message</returns>
        long GetMarkValue(long messageId);
    }
}

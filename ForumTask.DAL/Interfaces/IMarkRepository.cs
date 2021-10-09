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
        /// <para>
        /// If <paramref name="message"/> is null, throws <see cref="ArgumentNullException"/>
        /// </para>
        /// </summary>
        /// <param name="message">Message which mark is counted</param>
        /// <returns>Total mark of message</returns>
        /// <exception cref="ArgumentNullException"/>
        int GetMarkValue(Message message);
    }
}

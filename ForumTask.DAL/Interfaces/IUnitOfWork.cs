using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ForumTask.DAL.Interfaces {
    public interface IUnitOfWork { 
        ITopicRepository Topics { get; }
        IMessageRepository Messages { get; }
        IMarkRepository Marks { get; }

        void SaveChanges();
    }
}

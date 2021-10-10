﻿using ForumTask.DAL.Entities;
using ForumTask.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumTask.DAL.EF.Repositories {
    class MessageRepository : GenericRepository<Message>, IMessageRepository {
        public MessageRepository(ForumContext context):base(context) { }

        public IEnumerable<Message> GetTopOld(ulong topicId, int count, int offset) 
            => set.Where(m => m.TopicId == topicId).OrderBy(m => m.WriteTime).Skip(offset).Take(count);
        

        public int GetMessageCount(ulong topicId) 
            => set.Where(m => m.TopicId == topicId).Count();
    }
}
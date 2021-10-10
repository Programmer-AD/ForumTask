﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumTask.DAL.Entities {
    public enum MarkType {
        Negative=-1, Positive=1
    }
    public class Mark {
        //Will use (userId, messageId) as key
        public MarkType Type { get; set; }

        public uint UserId { get; set; }
        public User User { get; set; }

        public ulong MessageId { get; set; }
        public Message Message { get; set; }
    }
}
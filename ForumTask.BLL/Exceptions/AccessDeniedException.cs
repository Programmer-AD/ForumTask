using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumTask.BLL.Exceptions {
    [Serializable]
    public class AccessDeniedException : Exception {
        public AccessDeniedException():base("Access denied") { }
        public AccessDeniedException(string message) : base("Access denied: "+message) { }
        public AccessDeniedException(string message, Exception inner) : base("Access denied: "+message, inner) { }
        protected AccessDeniedException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

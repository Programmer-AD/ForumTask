using System;

namespace ForumTask.BLL.Exceptions {

    [Serializable]
    public class NotFoundException : Exception {
        public NotFoundException() : base("Not found") { }
        public NotFoundException(string message) : base("Not found: " + message) { }
        public NotFoundException(string message, Exception inner) : base("Not found: " + message, inner) { }
        protected NotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

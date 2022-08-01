using System;
using System.Collections.Generic;

namespace ForumTask.BLL.Identity
{

    [Serializable]
    public class IdentityException : Exception
    {
        public IdentityException() { }
        public IdentityException(IEnumerable<string> errCodes) : base(string.Join(", ", errCodes))
        {
            IdentityErrorCodes = errCodes;
        }
        public IdentityException(IEnumerable<string> errCodes, Exception innerException) : base(string.Join(", ", errCodes), innerException)
        {
            IdentityErrorCodes = errCodes;
        }
        public IdentityException(string message) : base(message) { }
        public IdentityException(string message, Exception inner) : base(message, inner) { }
        protected IdentityException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public IEnumerable<string> IdentityErrorCodes { get; set; }
    }
}

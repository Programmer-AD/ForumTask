using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumTask.DAL.Identity {

    [Serializable]
    public class IdentityException : Exception {
        public IEnumerable<string> IdentityErrorCodes;
        public IdentityException() { }
        public IdentityException(IEnumerable<string> errCodes):base(string.Join(", ", errCodes)) {
            IdentityErrorCodes = errCodes;
        }
        public IdentityException(IEnumerable<string> errCodes,Exception innerException) : base(string.Join(", ", errCodes),innerException) {
            IdentityErrorCodes = errCodes;
        }
        public IdentityException(string message) : base(message) { }
        public IdentityException(string message, Exception inner) : base(message, inner) { }
        protected IdentityException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

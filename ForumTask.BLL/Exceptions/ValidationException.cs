using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumTask.BLL.Exceptions {
    [Serializable]
    public class ValidationException : Exception {
        public ValidationException() : base("Validation error") { }
        public ValidationException(DAL.Identity.IdentityException e) : base("Validation error: " + e.Message) {
            foreach (var errCode in e.IdentityErrorCodes)
                switch (errCode) {
                    case "DuplicateEmail":
                        DuplicateEmail = true;
                        break;
                    case "DuplicateUserName":
                        DuplicateUserName = true;
                        break;
                    case "PasswordTooShort":
                        PasswordTooShort = true;
                        break;
                }
        }
        public ValidationException(string message) : base("Validation error: " + message) { }
        public ValidationException(string message, Exception inner) : base("Validation error:" + message, inner) { }
        protected ValidationException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public bool DuplicateEmail { get; }
        public bool DuplicateUserName { get; }
        public bool PasswordTooShort { get; }
    }
}

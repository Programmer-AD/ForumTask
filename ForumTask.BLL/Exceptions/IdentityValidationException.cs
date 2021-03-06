using System;

namespace ForumTask.BLL.Exceptions {
    [Serializable]
    public class IdentityValidationException : Exception {
        public IdentityValidationException() : base("Validation error") { }
        internal IdentityValidationException(Identity.IdentityException e) : base("Validation error: " + e.Message) {
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
        public IdentityValidationException(string message) : base("Validation error: " + message) { }
        public IdentityValidationException(string message, Exception inner) : base("Validation error:" + message, inner) { }
        protected IdentityValidationException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public bool DuplicateEmail { get; }
        public bool DuplicateUserName { get; }
        public bool PasswordTooShort { get; }
    }
}

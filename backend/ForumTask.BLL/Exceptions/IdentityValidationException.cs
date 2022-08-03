using Microsoft.AspNetCore.Identity;

namespace ForumTask.BLL.Exceptions
{
    public class IdentityValidationException : Exception
    {
        private const string ExceptionMessage = "Validation error! ";

        public IdentityValidationException() : base(ExceptionMessage) { }
        public IdentityValidationException(IdentityException exception) : base(ExceptionMessage + exception.Message)
        {
            foreach (string errorCode in exception.IdentityErrorCodes)
            {
                switch (errorCode)
                {
                    case nameof(IdentityErrorDescriber.DuplicateEmail):
                        DuplicateEmail = true;
                        break;
                    case nameof(IdentityErrorDescriber.DuplicateUserName):
                        DuplicateUserName = true;
                        break;
                    case nameof(IdentityErrorDescriber.PasswordTooShort):
                        PasswordTooShort = true;
                        break;
                }
            }
        }

        public bool DuplicateEmail { get; }
        public bool DuplicateUserName { get; }
        public bool PasswordTooShort { get; }
    }
}

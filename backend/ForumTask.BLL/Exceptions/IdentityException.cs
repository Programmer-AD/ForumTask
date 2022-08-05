namespace ForumTask.BLL.Exceptions
{
    public class IdentityException : Exception
    {
        public IdentityException() { }
        public IdentityException(IEnumerable<string> errCodes) : base(string.Join(", ", errCodes))
        {
            IdentityErrorCodes = errCodes;
        }

        public IEnumerable<string> IdentityErrorCodes { get; set; }
    }
}

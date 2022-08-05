namespace ForumTask.BLL.Exceptions
{
    public class AccessDeniedException : Exception
    {
        private const string MyMessage = "Access denied! ";

        public AccessDeniedException() : base(MyMessage) { }
        public AccessDeniedException(string message) : base(MyMessage + message) { }
    }
}

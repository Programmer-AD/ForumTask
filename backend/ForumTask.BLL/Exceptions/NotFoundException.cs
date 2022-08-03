namespace ForumTask.BLL.Exceptions
{
    public class NotFoundException : Exception
    {
        private const string ExceptionMessage = "Not found! ";

        public NotFoundException() : base(ExceptionMessage) { }
        public NotFoundException(string message) : base(ExceptionMessage + message) { }
    }
}

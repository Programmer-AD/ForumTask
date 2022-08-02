using ForumTask.BLL.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ForumTask.PL.Filters
{
    /// <summary>
    /// Handles exceptions and sets specific status codes and results
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ExceptionHandlerFilterAttribute : Attribute, IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            short statusCode = 500;
            object result = null;

            switch (context.Exception)
            {
                case AccessDeniedException e:
                    statusCode = 403;
                    result = e.Message;
                    break;
                case NotFoundException e:
                    statusCode = 404;
                    result = e.Message;
                    break;
                case IdentityValidationException e:
                    statusCode = 400;
                    result = new { e.PasswordTooShort, e.DuplicateUserName, e.DuplicateEmail };
                    break;
            }

            context.ExceptionHandled = true;
            context.Result = new ObjectResult(result) { StatusCode = statusCode };

            return Task.CompletedTask;
        }
    }
}

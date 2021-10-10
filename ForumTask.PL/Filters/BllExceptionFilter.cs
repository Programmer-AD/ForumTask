using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ForumTask.BLL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumTask.PL.Filters {
    /// <summary>
    /// Handles exception of BLL services and sets specific status codes and results
    /// </summary>
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Class)]
    public class BllExceptionFilter :Attribute, IExceptionFilter {
        public void OnException(ExceptionContext context) {
            short statusCode = 500;
            object result=null;
            switch (context.Exception) {
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
        }
    }
}

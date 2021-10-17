using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ForumTask.PL.Filters {
    /// <summary>
    /// Checks if model is valid, if not sets status code 400
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ModelValidFilterAttribute : Attribute, IActionFilter {
        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context) {
            if (!context.ModelState.IsValid)
                context.Result = new BadRequestResult();
        }
    }
}

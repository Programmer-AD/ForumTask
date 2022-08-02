using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace ForumTask.PL.Filters
{
    /// <summary>
    /// Checks if model is valid, if not sets status code 400
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ChangeSaverFilterAttribute : Attribute, IAsyncActionFilter
    {
        private readonly DbContext dbContext;

        public ChangeSaverFilterAttribute(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();
            await dbContext.SaveChangesAsync();
        }
    }
}

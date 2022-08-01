using ForumTask.BLL.Interfaces;
using ForumTask.BLL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ForumTask.BLL.DependencyInjection
{
    public static class BllServiceCollectionExtensions
    {
        public static void AddBll(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IIdentityManager, Identity.IdentityManager>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMarkService, MarkService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<ITopicService, TopicService>();
        }
    }
}

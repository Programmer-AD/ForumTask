using ForumTask.BLL.Interfaces;
using ForumTask.BLL.Services;
using ForumTask.DAL.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumTask.BLL.DependencyInjection {
    public static class BllServiceCollectionExtensions {
        public static void AddBll(this IServiceCollection services, IConfiguration config) {
            services.AddScoped<IIdentityManager, Identity.IdentityManager>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMarkService, MarkService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<ITopicService, TopicService>();
        }
    }
}

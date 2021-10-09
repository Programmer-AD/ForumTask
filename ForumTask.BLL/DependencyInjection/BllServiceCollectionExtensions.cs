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
            services.AddDal(config);


        }
    }
}

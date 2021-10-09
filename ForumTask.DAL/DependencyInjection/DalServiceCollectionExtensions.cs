using ForumTask.DAL.EF;
using ForumTask.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Identity.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ForumTask.DAL.Interfaces;

namespace ForumTask.DAL.DependencyInjection {
    public static class DalServiceCollectionExtensions {
        public static void AddDal(this IServiceCollection services,IConfiguration config) {
            services.AddDbContext<ForumContext>(opt => opt.UseSqlServer(
                config.GetConnectionString(ForumContextFactory.ConnectionStringName)));

            services.AddIdentity<User,IdentityRole<uint>>(opt => {
                opt.Password.RequireDigit = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;

                opt.User.RequireUniqueEmail = true;
            }).AddRoles<IdentityRole<uint>>().AddEntityFrameworkStores<ForumContext>();

            services.AddScoped<IUnitOfWork,UnitOfWork>();
        }
    }
}

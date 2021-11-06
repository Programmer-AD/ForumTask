using ForumTask.DAL.EF;
using ForumTask.DAL.Entities;
using ForumTask.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ForumTask.DAL.DependencyInjection {
    public static class DalServiceCollectionExtensions {
        public static void AddDal(this IServiceCollection services, IConfiguration config) {
            services.AddDbContext<ForumContext>(opt => opt.UseSqlServer(
                config.GetConnectionString("DbConnection")));

            services.AddIdentity<User, IdentityRole<int>>(opt => {
                opt.Password.RequireDigit = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;

                opt.User.RequireUniqueEmail = true;
            }).AddRoles<IdentityRole<int>>().AddEntityFrameworkStores<ForumContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}

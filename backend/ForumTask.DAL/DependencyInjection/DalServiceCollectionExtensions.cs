using ForumTask.DAL.EF;
using ForumTask.DAL.Entities;
using ForumTask.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ForumTask.DAL.DependencyInjection
{
    public static class DalServiceCollectionExtensions
    {
        public static void AddDal(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ForumContext>(GetDbContextConfigurator(config));
            services.AddScoped<DbContext>(provider => provider.GetService<ForumContext>());

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            services.AddIdentity<User, IdentityRole<long>>(ConfigureIdentity)
                .AddRoles<IdentityRole<long>>()
                .AddEntityFrameworkStores<ForumContext>();
        }

        private static Action<DbContextOptionsBuilder> GetDbContextConfigurator(IConfiguration config)
        {
            string connectionString = config.GetConnectionString("DbConnection");

            return options => options.UseSqlServer(connectionString);
        }

        private static void ConfigureIdentity(IdentityOptions options)
        {
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequiredLength = EntityConstants.User_Password_MinLength;

            options.User.RequireUniqueEmail = true;
        }
    }
}

using ForumTask.BLL.DependencyInjection;
using ForumTask.DAL.DependencyInjection;
using ForumTask.DAL.EF;
using ForumTask.PL.Filters;
using Microsoft.EntityFrameworkCore;

namespace ForumTask.PL
{
    public class Startup
    {
        private readonly IConfiguration config;

        public Startup(IConfiguration config)
        {
            this.config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDal(config);
            services.AddBll(config);

            services.AddSpaStaticFiles(config => config.RootPath = "wwwroot");
            services.AddControllers(options =>
            {
                options.Filters.Add<ExceptionHandlerFilterAttribute>();
                options.Filters.Add<ChangeSaverFilterAttribute>();
            });

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Events = new()
                {
                    OnRedirectToLogin = ctx =>
                    {
                        ctx.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    }
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ForumContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            db.Database.Migrate();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(config =>
            {
                config.Options.SourcePath = "wwwroot";
            });
        }
    }
}

using System.Threading.Tasks;
using ForumTask.BLL.DependencyInjection;
using ForumTask.DAL.DependencyInjection;
using ForumTask.DAL.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ForumTask.PL {
    public class Startup {
        private readonly IConfiguration config;

        public Startup(IConfiguration config) {
            this.config = config;
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddDal(config);
            services.AddBll(config);

            services.AddSpaStaticFiles(conf => conf.RootPath = "wwwroot");
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new() { 
                    Title = "Forum task api",
                    Version = "v1",
                    Description = "API written using ASP.NET Core web api for simple forum",
                });
            });

            services.ConfigureApplicationCookie(opt => {
                opt.Events = new() {
                    OnRedirectToLogin = ctx => {
                        ctx.Response.StatusCode = 401;
                        return Task.FromResult(0);
                    }
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ForumContext db) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            db.Database.Migrate();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseSwagger(options =>
            {
                options.RouteTemplate = "api/doc/{documentname}/swagger.json";
            });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/api/doc/v1/swagger.json", "v1");
                options.RoutePrefix = "api/doc";
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            app.UseSpa(config => {
                config.Options.SourcePath = "wwwroot";
            });
        }
    }
}

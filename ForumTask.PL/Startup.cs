using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumTask.BLL.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            services.AddBll(config);

            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapGet("/", async context => {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}

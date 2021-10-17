using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ForumTask.DAL.EF {
    class ForumContextFactory : IDesignTimeDbContextFactory<ForumContext> {
        public const string ConnectionStringName = "DbConnection";

        public ForumContext CreateDbContext(string[] args) {
            ConfigurationBuilder builder = new();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<ForumContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString(ConnectionStringName));
            return new ForumContext(optionsBuilder.Options);
        }
    }
}

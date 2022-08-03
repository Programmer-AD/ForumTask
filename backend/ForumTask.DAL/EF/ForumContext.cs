using ForumTask.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ForumTask.DAL.EF
{
    public class ForumContext : IdentityDbContext<User, IdentityRole<long>, long>
    {
        private const int adminUserId = 1;
        private const string adminDefaultEmail = "admin@forum.here";
        private const string adminDefaultPassword = "admin_pass";
        private const string adminUserName = "Admin";

        public ForumContext(DbContextOptions<ForumContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ForumContext).Assembly);

            SeedIdentity(builder);
        }

        private static void SeedIdentity(ModelBuilder builder)
        {
            SeedRoles(builder);

            SeedUsers(builder);

            SeedUserRoleRelations(builder);
        }

        private static void SeedUserRoleRelations(ModelBuilder builder)
        {
            const long adminRoleId = 3;

            var userRoleRelation = new IdentityUserRole<long>() { UserId = adminUserId, RoleId = adminRoleId };

            builder.Entity<IdentityUserRole<long>>().HasData(userRoleRelation);
        }

        private static void SeedRoles(ModelBuilder builder)
        {
            string[] roleNames = new[] { RoleNames.User, RoleNames.Moderator, RoleNames.Admin };
            var roles = roleNames.Select((roleName, i) => CreateRole(i + 1, roleName));

            builder.Entity<IdentityRole<long>>().HasData(roles);
        }

        private static void SeedUsers(ModelBuilder builder)
        {
            var hasher = new PasswordHasher<User>();

            var adminUser = CreateUser(adminUserId, adminUserName, adminDefaultEmail, adminDefaultPassword, hasher);

            builder.Entity<User>().HasData(adminUser);
        }

        private static User CreateUser(long id, string userName, string email, string password, PasswordHasher<User> hasher)
        {
            var user = new User()
            {
                Id = id,
                UserName = userName,
                NormalizedUserName = userName.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                EmailConfirmed = true,
                RegisterDate = DateTime.UtcNow,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            user.PasswordHash = hasher.HashPassword(user, password);

            return user;
        }

        private static IdentityRole<long> CreateRole(long id, string roleName)
        {
            return new(roleName) { Id = id, NormalizedName = roleName.ToUpper() };
        }
    }
}

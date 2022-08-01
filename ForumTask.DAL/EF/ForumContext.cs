using System;
using ForumTask.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ForumTask.DAL.EF
{
    public class ForumContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Mark> Marks { get; set; }

        public ForumContext(DbContextOptions<ForumContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Mark>().HasKey(m => new { m.UserId, m.MessageId });
            builder.Entity<Topic>().HasOne(t => t.Creator).WithMany().OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Message>().HasOne(t => t.Author).WithMany().OnDelete(DeleteBehavior.SetNull);

            //Initializing identity db
            builder.Entity<IdentityRole<int>>().HasData(CreateRole(1, "User"), CreateRole(2, "Moderator"), CreateRole(3, "Admin"));
            PasswordHasher<User> hasher = new();
            builder.Entity<User>().HasData(CreateUser(1, "Admin", "admin@forum.here", "admin_pass", hasher));
            builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>() { UserId = 1, RoleId = 3 });

            static IdentityRole<int> CreateRole(int id, string roleName)
            {
                return new(roleName) { NormalizedName = roleName.ToUpper(), Id = id };
            }

            static User CreateUser(int id, string userName, string email, string password, PasswordHasher<User> hasher)
            {
                User user = new(userName)
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
        }
    }
}

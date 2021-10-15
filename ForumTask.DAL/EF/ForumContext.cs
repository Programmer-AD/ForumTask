using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ForumTask.DAL.Entities;

namespace ForumTask.DAL.EF {
    class ForumContext:IdentityDbContext<User,IdentityRole<int>,int> {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Mark> Marks { get; set; }

        public ForumContext(DbContextOptions<ForumContext> options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            builder.Entity<Mark>().HasKey(m => new { m.UserId, m.MessageId });

            //Initializing identity db
            builder.Entity<IdentityRole<int>>().HasData(CreateRole(1,"User"), CreateRole(2,"Moderator"),CreateRole(3,"Admin"));
            PasswordHasher<User> hasher = new();
            builder.Entity<User>().HasData(CreateUser(1,"Admin","admin@forum.here","admin_pass",hasher));
            builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>() {UserId=1, RoleId = 3});

            static IdentityRole<int> CreateRole(int id, string roleName)
                => new(roleName) { NormalizedName = roleName.ToUpper(), Id = id };
            static User CreateUser(int id, string userName, string email, string password, PasswordHasher<User> hasher) { 
                User user= new(userName) {
                    Id = id,
                    UserName = userName,
                    NormalizedUserName = userName.ToUpper(),
                    Email = email,
                    NormalizedEmail = email.ToUpper(),
                    EmailConfirmed = true,
                    RegisterDate = DateTime.UtcNow,
                    SecurityStamp= Guid.NewGuid().ToString(),
                };
                user.PasswordHash = hasher.HashPassword(user, password);
                return user;
            }
        }
    }
}

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
    class ForumContext:IdentityDbContext<User,IdentityRole<uint>,uint> {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Mark> Marks { get; set; }

        public ForumContext(DbContextOptions<ForumContext> options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            builder.Entity<Mark>().HasKey(m => new { m.UserId, m.MessageId });

            //Initializing identity db
            builder.Entity<IdentityRole<uint>>().HasData(CreateRole(1,"User"), CreateRole(2,"Moderator"),CreateRole(3,"Admin"));
            PasswordHasher<User> hasher = new();
            builder.Entity<User>().HasData(CreateUser(1,"Admin","admin@forum.here","admin_pass",hasher));
            builder.Entity<IdentityUserRole<uint>>().HasData(new IdentityUserRole<uint>() {UserId=1, RoleId = 3});

            static IdentityRole<uint> CreateRole(uint id, string roleName)
                => new(roleName) { NormalizedName = roleName.ToUpper(), Id = id };
            static User CreateUser(uint id, string userName, string email, string password, PasswordHasher<User> hasher) { 
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

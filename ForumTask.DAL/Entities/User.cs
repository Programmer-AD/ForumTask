using System;
using Microsoft.AspNetCore.Identity;

namespace ForumTask.DAL.Entities {
    public class User : IdentityUser<int> {
        public DateTime RegisterDate { get; set; }
        public bool IsBanned { get; set; }

        public User() { }
        public User(string userName) : base(userName) { }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumTask.DAL.Entities {
    public class User:IdentityUser<uint> {
        public DateTime RegisterDate { get; set; }
        public bool IsBanned { get; set; }

        public User() { }
        public User(string userName) : base(userName) { }
    }
}

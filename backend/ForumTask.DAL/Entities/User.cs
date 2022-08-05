using Microsoft.AspNetCore.Identity;

namespace ForumTask.DAL.Entities
{
    public class User : IdentityUser<long>
    {
        public DateTime RegisterDate { get; set; }
        public bool IsBanned { get; set; }
    }
}

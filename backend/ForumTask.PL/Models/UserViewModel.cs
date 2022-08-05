namespace ForumTask.PL.Models
{
    public class UserViewModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsBanned { get; set; }
        public string RoleName { get; set; }
    }
}

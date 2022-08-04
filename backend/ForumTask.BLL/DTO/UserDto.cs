namespace ForumTask.BLL.DTO
{
    public class UserDto
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsBanned { get; set; }

        public RoleEnum MaxRole { get; set; }
    }
}

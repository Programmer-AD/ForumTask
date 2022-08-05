using ForumTask.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForumTask.DAL.EF.EntityConfigs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.UserName)
                .HasMaxLength(EntityConstants.User_UserName_MaxLength);

            builder.Property(x => x.Email)
                .HasMaxLength(EntityConstants.User_Email_MaxLength);
        }
    }
}

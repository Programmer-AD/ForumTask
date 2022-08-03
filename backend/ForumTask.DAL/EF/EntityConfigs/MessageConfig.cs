using ForumTask.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForumTask.DAL.EF.EntityConfigs
{
    public class MessageConfig : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(x => x.Text)
                .HasMaxLength(EntityConstants.Message_Text_MaxLength)
                .IsRequired();

            builder.HasOne(x => x.Author).WithMany().OnDelete(DeleteBehavior.SetNull);
        }
    }
}

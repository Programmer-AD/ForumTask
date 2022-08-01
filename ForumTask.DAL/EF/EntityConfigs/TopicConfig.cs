using ForumTask.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForumTask.DAL.EF.EntityConfigs
{
    public class TopicConfig : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.Property(x => x.Title)
                .HasMaxLength(EntityConstants.Topic_Title_MaxLength)
                .IsRequired();

            builder.HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(x => x.Messages).WithOne(x => x.Topic).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

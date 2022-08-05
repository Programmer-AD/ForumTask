using ForumTask.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForumTask.DAL.EF.EntityConfigs
{
    public class MarkConfig : IEntityTypeConfiguration<Mark>
    {
        public void Configure(EntityTypeBuilder<Mark> builder)
        {
            builder.HasKey(x => new { x.UserId, x.MessageId });
        }
    }
}

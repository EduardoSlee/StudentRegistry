using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentRegistry.Repositories.Students
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> entity)
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getutcdate())");
            entity.HasIndex(e => new { e.Name, e.LastName }).IsUnique(false);
        }
    }
}

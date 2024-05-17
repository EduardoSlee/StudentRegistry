using Microsoft.EntityFrameworkCore;
using StudentRegistry.Repositories.Students;

namespace StudentRegistry.Repositories.Data
{
    public class StudentRegistryDbContext : DbContext
    {
        public StudentRegistryDbContext(DbContextOptions<StudentRegistryDbContext> options)
            : base(options)
        { }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
        }
    }
}

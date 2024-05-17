using Microsoft.EntityFrameworkCore;
using StudentRegistry.Repositories.Data;
using System.Linq;

namespace StudentRegistry.Repositories.Students
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly StudentRegistryDbContext _studentRegistryDbContext;

        public StudentsRepository(StudentRegistryDbContext studentRegistryDbContext)
        {
            _studentRegistryDbContext = studentRegistryDbContext;
        }

        public async Task AddStudentAsync(Student student)
        {
            _studentRegistryDbContext.Students.Add(student);
            await _studentRegistryDbContext.SaveChangesAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _studentRegistryDbContext.Students
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateStudentAsync(Student student)
        {
            _studentRegistryDbContext.Update(student);
            await _studentRegistryDbContext.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(Student student)
        {
            _studentRegistryDbContext.Remove(student);
            await _studentRegistryDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync(DateTime? createDate)
        {
            return await _studentRegistryDbContext.Students
                .Where(x => createDate == null || x.CreateDate.Date == createDate.Value.Date)
                .ToListAsync();
        }
    }
}

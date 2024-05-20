using StudentRegistry.Services.Students.Models;

namespace StudentRegistry.Services.Students
{
    public interface IStudentsService
    {
        Task<StudentResult> AddStudentAsync(StudentInput studentInput);

        Task<IEnumerable<StudentResult>> GetAllStudentsAsync();

        Task<StudentResult?> GetStudentByIdAsync(int id);

        Task<StudentResult> UpdateStudentAsync(int id, StudentInput studentInput);

        Task DeleteStudentAsync(int id);

        Task<byte[]> GetStudentsExcelReportAsync(DateTime? createDate);
    }
}

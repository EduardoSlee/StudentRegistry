namespace StudentRegistry.Repositories.Students
{
    public interface IStudentsRepository
    {
        Task AddStudentAsync(Student student);

        Task<Student?> GetStudentByIdAsync(int id);

        Task UpdateStudentAsync(Student student);

        Task DeleteStudentAsync(Student student);

        Task<IEnumerable<Student>> GetStudentsAsync(DateTime? createDate);
    }
}

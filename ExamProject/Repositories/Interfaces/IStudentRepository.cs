using ExamProject.Models.Entities;

namespace ExamProject.Repositories.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<IEnumerable<Student>> GetStudentsWithBranchTrackAsync();
        Task<Student> GetStudentWithBranchTrackAsync(int studentId);
        Task<bool> StudentExistsAsync(int studentId);

    }
}

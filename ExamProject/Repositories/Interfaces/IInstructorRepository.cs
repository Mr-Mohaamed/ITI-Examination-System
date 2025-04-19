using ExamProject.Models.Entities;

namespace ExamProject.Repositories.Interfaces
{
    public interface IInstructorRepository : IRepository<Instructor>
    {
        Task<Instructor> GetInstructorWithCoursesAndBranchAndTrackAsync(int id);
        Task<bool> InstructorExistsAsync(int instructorId);
    }
}

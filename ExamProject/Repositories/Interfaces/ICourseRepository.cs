using ExamProject.Models.Entities;

namespace ExamProject.Repositories.Interfaces
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<Course> GetCourseWithBranchAndTrackAndInstructorsAndTopicsAsync(int courseId);
        Task<bool> CourseExistsAsync(int courseId);
    }

}

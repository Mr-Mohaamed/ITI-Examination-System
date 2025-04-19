using ExamProject.Models.Data;
using ExamProject.Models.Entities;
using ExamProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.Repositories.Implementations
{


    public class CourseRepository(AppDbContext context) : BaseRepository<Course>(context), ICourseRepository
    {
        public async Task<Course> GetCourseWithBranchAndTrackAndInstructorsAndTopicsAsync(int courseId)
            => await _context.Courses
                .Include(c => c.CourseTracks)
                    .ThenInclude(ct => ct.CourseAssignments)
                        .ThenInclude(ca => ca.Instructor)
                .Include(c => c.CourseTracks)
                    .ThenInclude(ct => ct.CourseAssignments)
                        .ThenInclude(ca => ca.Branch)
                .Include(c => c.CourseTracks)
                    .ThenInclude(ct => ct.Track)
                .Include(c => c.CourseTopics)
                    .ThenInclude(tc => tc.Topic)
                .FirstOrDefaultAsync(c => c.Id == courseId);
        public async Task<bool> CourseExistsAsync(int courseId)
            => await _context.Courses
                .AnyAsync(c => c.Id == courseId);

    }

}

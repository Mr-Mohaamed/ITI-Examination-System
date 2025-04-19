using ExamProject.Models.Data;
using ExamProject.Models.Entities;
using ExamProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.Repositories.Implementations
{
    public class InstructorRepository(AppDbContext context) : BaseRepository<Instructor>(context), IInstructorRepository
    {
        public async Task<Instructor> GetInstructorWithCoursesAndBranchAndTrackAsync(int id)
            => await _context.Instructors
                .Include(i => i.CourseAssignments)
                    .ThenInclude(ca => ca.CourseTrack)
                        .ThenInclude(ct => ct.Course)
                .Include(i => i.CourseAssignments)
                    .ThenInclude(ca => ca.CourseTrack)
                        .ThenInclude(ct => ct.Track)
                .Include(i => i.CourseAssignments)
                    .ThenInclude(ca => ca.Branch)
                .FirstOrDefaultAsync(i=>i.Id == id);
        public async Task<bool> InstructorExistsAsync(int instructorId)
            => await _context.Instructors
                .AnyAsync(i => i.Id == instructorId);
    }
}

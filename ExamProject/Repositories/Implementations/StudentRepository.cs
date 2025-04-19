using ExamProject.Models.Data;
using ExamProject.Models.Entities;
using ExamProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.Repositories.Implementations
{
    public class StudentRepository(AppDbContext context) : BaseRepository<Student>(context), IStudentRepository
    {
        public async Task<IEnumerable<Student>> GetStudentsWithBranchTrackAsync()
            => await _context.Students
                .Include(s => s.BranchTrack)
                    .ThenInclude(bt => bt.Branch)
                .Include(s => s.BranchTrack)
                    .ThenInclude(bt => bt.Track)
                .ToListAsync();
        public async Task<Student> GetStudentWithBranchTrackAsync(int studentId)
            => await _context.Students
                .Include(s => s.BranchTrack)
                    .ThenInclude(bt => bt.Branch)
                .Include(s => s.BranchTrack)
                    .ThenInclude(bt => bt.Track)
                .FirstOrDefaultAsync(s => s.Id == studentId);
        public async Task<bool> StudentExistsAsync(int studentId)
            => await _context.Students
                .AnyAsync(s => s.Id == studentId);
    }
}

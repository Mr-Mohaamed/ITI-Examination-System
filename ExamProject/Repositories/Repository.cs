//// BranchTrackRepository.cs
//using ExamProject.Models.Data;
//using ExamProject.Models.Entities;
//using ExamProject.Repositories;
//using Microsoft.EntityFrameworkCore;

//public interface IBranchTrackRepository : IRepository<BranchTrack>
//{
//    Task<bool> ExistsAsync(int branchId, int trackId);
//    Task<IEnumerable<Student>> GetStudentsAsync(int branchId, int trackId);
//}

//public class BranchTrackRepository : BaseRepository<BranchTrack>, IBranchTrackRepository
//{
//    public BranchTrackRepository(AppDbContext context) : base(context) { }

//    public async Task<bool> ExistsAsync(int branchId, int trackId)
//        => await _context.BranchTracks
//            .AnyAsync(bt => bt.BranchId == branchId && bt.TrackId == trackId);

//    public async Task<IEnumerable<Student>> GetStudentsAsync(int branchId, int trackId)
//        => await _context.Students
//            .Where(s => s.BranchId == branchId && s.TrackId == trackId)
//            .ToListAsync();
//}

//// CourseAssignmentRepository.cs
//public interface ICourseAssignmentRepository : IRepository<CourseAssignment>
//{
//    Task<bool> InstructorHasAssignmentAsync(int instructorId);
//    Task<IEnumerable<CourseAssignment>> GetByBranchTrackAsync(int branchId, int trackId);
//}

//public class CourseAssignmentRepository : BaseRepository<CourseAssignment>, ICourseAssignmentRepository
//{
//    public CourseAssignmentRepository(AppDbContext context) : base(context) { }

//    public async Task<bool> InstructorHasAssignmentAsync(int instructorId)
//        => await _context.CourseAssignments
//            .AnyAsync(ca => ca.InstructorId == instructorId);

//    public async Task<IEnumerable<CourseAssignment>> GetByBranchTrackAsync(int branchId, int CourseTrack)
//        => await _context.CourseAssignments
//            .Where(ca => ca.BranchId == branchId && ca.CourseTrackId == CourseTrack)
//            .Include(ca => ca.CourseTrack)
//                .ThenInclude(ct => ct.Track)
//            .Include(ca => ca.CourseTrack)
//                .ThenInclude(ct => ct.Course)
//            .Include(ca => ca.Branch)
//            .Include(ca => ca.Instructor)
//            .ToListAsync();
//}
//public interface IBranchRepository : IRepository<Branch>
//{
//    Task<IEnumerable<Branch>> GetAllBranchesWithTracksAsync();
//    Task<Branch> GetBranchWithTracksAndStudentsAsync(int branchId);
//    Task<Branch> GetBranchWithTracksAsync(int branchId);
//    Task<bool> BranchExistsAsync(int branchId);

//}
//public class BranchRepository(AppDbContext context) : BaseRepository<Branch>(context), IBranchRepository
//{
//    public async Task<IEnumerable<Branch>> GetAllBranchesWithTracksAsync()
//        => await _context.Branches
//            .Include(b => b.BranchTracks)
//                .ThenInclude(bt => bt.Track)
//            .Include(b => b.BranchTracks)
//                .ThenInclude(bt => bt.Students)
//            .ToListAsync();
//    public async Task<Branch> GetBranchWithTracksAndStudentsAsync(int branchId)
//        => await _context.Branches
//            .Include(b => b.BranchTracks)
//                .ThenInclude(bt => bt.Track)
//            .Include(b => b.BranchTracks)
//                .ThenInclude(bt => bt.Students)
//            .FirstOrDefaultAsync(b => b.Id == branchId);

//    public async Task<Branch> GetBranchWithTracksAsync(int branchId)
//        => await _context.Branches
//            .Include(b => b.BranchTracks)
//                .ThenInclude(bt => bt.Track)
//            .FirstOrDefaultAsync(b => b.Id == branchId);
//    public async Task<bool> BranchExistsAsync(int branchId)
//        => await _context.Branches
//            .AnyAsync(b => b.Id == branchId);
//}

//// TrackRepository.cs
//public interface ITrackRepository : IRepository<Track>
//{
//    Task<IEnumerable<Track>> GetAllTracksWithBranchesAsync();
//    Task<Track> GetTrackWithBranchesAndStudentsAsync(int trackId);
//    Task<bool> TrackExistsAsync(int trackId);
//}
//public class TrackRepository(AppDbContext context) : BaseRepository<Track>(context), ITrackRepository
//{
//    public async Task<IEnumerable<Track>> GetAllTracksWithBranchesAsync()
//        => await _context.Tracks
//            .Include(t => t.BranchTracks)
//                .ThenInclude(bt => bt.Branch)
//            .ToListAsync();
//    public async Task<Track> GetTrackWithBranchesAndStudentsAsync(int trackId)
//        => await _context.Tracks
//            .Include(t => t.BranchTracks)
//                .ThenInclude(bt => bt.Branch)
//            .Include(t => t.BranchTracks)
//                .ThenInclude(bt => bt.Students)
//            .FirstOrDefaultAsync(t => t.Id == trackId);
//    public async Task<bool> TrackExistsAsync(int trackId)
//        => await _context.Tracks
//            .AnyAsync(t => t.Id == trackId);
//}
//// CourseRepository.cs
//public interface ICourseRepository : IRepository<Course>
//{
//    Task<Course> GetCourseWithBranchAndTrackAndInstructorsAndTopicsAsync(int courseId);
//    Task<bool> CourseExistsAsync(int courseId);
//}
//public class CourseRepository(AppDbContext context) : BaseRepository<Course>(context), ICourseRepository
//{
//    public async Task<Course> GetCourseWithBranchAndTrackAndInstructorsAndTopicsAsync(int courseId)
//        => await _context.Courses
//            .Include(c => c.CourseTracks)
//                .ThenInclude(ct => ct.CourseAssignments)
//                    .ThenInclude(ca => ca.Instructor)
//            .Include(c => c.CourseTracks)
//                .ThenInclude(ct => ct.CourseAssignments)
//                    .ThenInclude(ca => ca.Branch)
//            .Include(c => c.CourseTopics)
//                    .ThenInclude(tc => tc.Topic)
//            .FirstOrDefaultAsync(c => c.Id == courseId);
//    public async Task<bool> CourseExistsAsync(int courseId)
//        => await _context.Courses
//            .AnyAsync(c => c.Id == courseId);

//}
//// InstructorRepository.cs
//public interface IInstructorRepository : IRepository<Instructor>
//{
//    Task<IEnumerable<Instructor>> GetInstructorsWithCoursesAsync();
//    Task<bool> InstructorExistsAsync(int instructorId);
//}
//public class InstructorRepository(AppDbContext context) : BaseRepository<Instructor>(context), IInstructorRepository
//{
//    public async Task<IEnumerable<Instructor>> GetInstructorsWithCoursesAsync()
//        => await _context.Instructors
//            .Include(i => i.CourseAssignments)
//                .ThenInclude(ca => ca.CourseTrack)
//                    .ThenInclude(ct => ct.Course)
//            .ToListAsync();
//    public async Task<bool> InstructorExistsAsync(int instructorId)
//        => await _context.Instructors
//            .AnyAsync(i => i.Id == instructorId);
//}
//// StudentRepository.cs
//public interface IStudentRepository : IRepository<Student>
//{
//    Task<IEnumerable<Student>> GetStudentsWithBranchTrackAsync();
//    Task<bool> StudentExistsAsync(int studentId);

//}
//public class StudentRepository(AppDbContext context) : BaseRepository<Student>(context), IStudentRepository
//{
//    public async Task<IEnumerable<Student>> GetStudentsWithBranchTrackAsync()
//        => await _context.Students
//            .Include(s => s.BranchTrack) 
//                .ThenInclude(bt => bt.Branch)
//            .Include(s => s.BranchTrack)
//                .ThenInclude(bt => bt.Track)
//            .ToListAsync();
//    public async Task<bool> StudentExistsAsync(int studentId)
//        => await _context.Students
//            .AnyAsync(s => s.Id == studentId);
//}
//// TopicRepository.cs
//public interface ITopicRepository : IRepository<Topic>
//{
//    Task<IEnumerable<Topic>> GetTopicsWithCoursesAsync();
//    Task<bool> TopicExistsAsync(int topicId);
//}
//public class TopicRepository(AppDbContext context) : BaseRepository<Topic>(context), ITopicRepository
//{
//    public async Task<IEnumerable<Topic>> GetTopicsWithCoursesAsync()
//        => await _context.Topics
//            .Include(t => t.CourseTopics)
//                .ThenInclude(tc => tc.Course)
//            .ToListAsync();
//    public async Task<bool> TopicExistsAsync(int topicId)
//        => await _context.Topics
//            .AnyAsync(t => t.Id == topicId);
//}

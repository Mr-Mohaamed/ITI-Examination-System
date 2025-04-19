using ExamProject.Models.Data;
using ExamProject.Models.Entities;
using ExamProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.Repositories.Implementations
{
    public class TopicRepository(AppDbContext context) : BaseRepository<Topic>(context), ITopicRepository
    {
        public async Task<Topic> GetTopicWithCoursesAsync(int id)
            => await _context.Topics
                .Include(t => t.CourseTopics)
                    .ThenInclude(tc => tc.Course)
                .FirstOrDefaultAsync(t => t.Id == id);
        public async Task<bool> TopicExistsAsync(int topicId)
        {
            return await _context.Topics.AnyAsync(t => t.Id == topicId);
        }
    }
}

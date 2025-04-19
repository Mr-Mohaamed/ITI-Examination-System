using ExamProject.Models.Entities;

namespace ExamProject.Repositories.Interfaces
{
    public interface ITopicRepository : IRepository<Topic>
    {
        Task<Topic> GetTopicWithCoursesAsync(int id);
        Task<bool> TopicExistsAsync(int topicId);
    }
}

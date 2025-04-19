using ExamProject.Models.DTOs.BranchDTOs;
using ExamProject.Models.DTOs.CoursesDTOs;
using ExamProject.Models.DTOs.TopicDTOs;
using ExamProject.Models.Entities;

namespace ExamProject.Services.Interfaces
{
    public interface ITopicService
    {
        Task<IEnumerable<IndexTopicDTO>> IndexTopics();
        Task<GetTopicDTO> GetTopic(int id);
        Task<bool> CreateTopic(CreateTopicDTO topicDto);
        Task<EditTopicDTO> EditTopic(int id);
        Task<bool> UpdateTopic(EditTopicDTO topicDto);
        Task<bool> DeleteTopic(int id);

        // Select List
        Task<IEnumerable<TopicSelectListDTO>> TopicsSelectList();
    }
}

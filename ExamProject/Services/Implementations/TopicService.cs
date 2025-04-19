using ExamProject.Models.DTOs.BranchDTOs;
using ExamProject.Models.DTOs.CoursesDTOs;
using ExamProject.Models.DTOs.TopicDTOs;
using ExamProject.Models.Entities;
using ExamProject.Repositories.Implementations;
using ExamProject.Repositories.Interfaces;
using ExamProject.Services.Interfaces;

namespace ExamProject.Services.Implementations
{
    public class TopicService(IRepository<Topic> repository) : ITopicService
    {
        private readonly IRepository<Topic> _repository = repository;

        public async Task<IEnumerable<IndexTopicDTO>> IndexTopics()
        {
            var topics = await repository.GetAllAsync();
            var topicDtos = topics.Select(topic => new IndexTopicDTO
            {
                Id = topic.Id,
                Name = topic.Name,
            });
            return topicDtos;

        }
        public async Task<GetTopicDTO> GetTopic(int id)
        {
            var topic = await repository.GetByIdWithNestedIncludesAsync(id, "CourseTopics.Course");
            if (topic == null)
            {
                return null;
            }
            var topicDto = new GetTopicDTO
            {
                Id = topic.Id,
                Name = topic.Name,
                Description = topic.Description,
                Courses = topic.CourseTopics.Select(ct => new GetTopicCourseDTO
                {
                    Id = ct.CourseId,
                    Name = ct.Course.Name
                }).ToList()
            };
            return topicDto;

        }
        public async Task<bool> CreateTopic(CreateTopicDTO topicDto)
        {
            var topic = new Topic
            {
                Name = topicDto.Name,
                Description = topicDto.Description,
            };
            await _repository.AddAsync(topic);
            return true;
        }
        public async Task<EditTopicDTO> EditTopic(int id)
        {
            var topic = await _repository.GetByIdAsync(id);
            if (topic == null)
            {
                return null;
            }
            var topicDto = new EditTopicDTO
            {
                Id = topic.Id,
                Name = topic.Name,
                Description = topic.Description,
            };
            return topicDto;

        }

        public async Task<bool> UpdateTopic(EditTopicDTO topicDto)
        {
            var topic = await _repository.GetByIdAsync(topicDto.Id);
            if (topic == null)
            {
                return false;
            }
            topic.Name = topicDto.Name;
            topic.Description = topicDto.Description;
            await _repository.UpdateAsync(topic);

            return true;
        }
        public async Task<bool> DeleteTopic(int id)
        {
            var topic = await _repository.GetByIdAsync(id);
            if (topic == null)
            {
                return false;
            }
            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<TopicSelectListDTO>> TopicsSelectList()
        {
            var topics = await _repository.GetAllAsync();
            return topics.Select(t => new TopicSelectListDTO
            {
                TopicId = t.Id,
                TopicName = t.Name
            }).ToList();
        }
    }
}

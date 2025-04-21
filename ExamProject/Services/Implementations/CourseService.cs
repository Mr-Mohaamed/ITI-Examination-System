using ExamProject.Models.DTOs.BranchDTOs;
using ExamProject.Models.DTOs.CoursesDTOs;
using ExamProject.Models.DTOs.TrackDTOs;
using ExamProject.Models.Entities;
using ExamProject.Services.Interfaces;

namespace ExamProject.Services.Implementations
{
    public class CourseService(IRepository<Course> courseRepository, IRepository<Topic> topicRepository) : ICourseService
    {
        private readonly IRepository<Course> _courseRepository = courseRepository;
        private readonly IRepository<Topic> _topicRepository = topicRepository;

        public async Task<IEnumerable<IndexCourseDTO>> IndexCourses()
        {
            var courses = await courseRepository.GetAllAsync();
            var courseDtos = courses.Select(course => new IndexCourseDTO
            {
                Id = course.Id,
                Name = course.Name,
            });
            return courseDtos;

        }
        public async Task<GetCourseDTO> GetCourse(int id)
        {
            var course = await courseRepository.GetByIdWithNestedIncludesAsync(id, [
                "CourseTopics.Topic",
                "CourseTracks.Track",
                "CourseTracks.CourseAssignments.Instructor",
                "CourseTracks.CourseAssignments.Branch"
                ]);
            if (course == null)
            {
                return null;
            }
            var courseDto = new GetCourseDTO
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Duration = course.Duration,
                Topics = course.CourseTopics.Select(ct => new GetCourseTopicDTO
                {
                    Id = ct.Topic.Id,
                    Name = ct.Topic.Name,
                }).ToList(),
                Assignments = course.CourseTracks
                    .SelectMany(ct => ct.CourseAssignments.Select(ca => new GetCoursesInstructorsDTO
                    {
                        InstructorId = ca.Instructor.Id,
                        InstructorName = ca.Instructor.Name,
                        BranchId = ca.Branch.Id,
                        BranchName = ca.Branch.Name,
                        TrackId = ct.Track.Id,
                        TrackName = ct.Track.Name
                    }))
                    .ToList()
            };
            return courseDto;
        }
        public async Task<bool> CreateCourse(CreateCourseDTO courseDto)
        {
            var course = new Course
            {
                Name = courseDto.Name,
                Description = courseDto.Description,
                Duration = courseDto.Duration
            };
            await _courseRepository.AddAsync(course);
            return true;
        }
        public async Task<EditCourseDTO> EditCourse(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return null;
            }
            var courseDto = new EditCourseDTO
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Duration = course.Duration,
            };
            return courseDto;

        }

        public async Task<bool> UpdateCourse(EditCourseDTO courseDto)
        {
            var course = await _courseRepository.GetByIdAsync(courseDto.Id);
            if (course == null)
            {
                return false;
            }
            course.Name = courseDto.Name;
            course.Description = courseDto.Description;
            course.Duration = courseDto.Duration;
            _courseRepository.Update(course);
            return true;
        }
        public async Task<bool> DeleteCourse(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return false;
            }
            await _courseRepository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> UpdateCourseTopics(int id, List<int> ToBeAdded, List<int> ToBeRemoved)
        {

            if (ToBeAdded == null && ToBeRemoved == null)
                return false;
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
                return false;
            // Add new topics
            foreach (var topicId in ToBeAdded)
            {
                var topic = await _topicRepository.GetByIdAsync(topicId);
                if (topic != null)
                {
                    course.CourseTopics.Add(new CourseTopic { CourseId = course.Id, TopicId = topicId });
                }
            }
            // Remove topics
            foreach (var topicId in ToBeRemoved)
            {
                var courseTopicToRemove = course.CourseTopics
                    .FirstOrDefault(ct => ct.TopicId == topicId && ct.CourseId == course.Id);

                if (courseTopicToRemove != null)
                {
                    course.CourseTopics.Remove(courseTopicToRemove);
                }
                //var topic = await _topicRepository.GetByIdAsync(topicId);
                //if (topic != null)
                //{
                //    course.CourseTopics.Remove(new CourseTopic { CourseId = course.Id, TopicId = topicId });
                //}
            }
            await _courseRepository.UpdateAsync(course);
            return true;
        }

        public async Task<IEnumerable<CourseTopicsSelectListDTO>> AllCoursesWithTopicsSelectList()
        {
            var courses = await _courseRepository.GetAllWithNestedIncludesAsync("CourseTopics.Topic");
            return courses.Select(b => new CourseTopicsSelectListDTO
            {
                CourseId = b.Id,
                CourseName = b.Name,
                Topics = b.CourseTopics.Select(bt => new TopicSelectListDTO
                {
                    TopicId = bt.Topic.Id,
                    TopicName = bt.Topic.Name,
                }).ToList()
            });
        }

        public async Task<CourseTopicsSelectListDTO> CourseWithTopicsSelectList(int id)
        {
            var course = await _courseRepository.GetByIdWithNestedIncludesAsync(id, "CourseTopics.Topic");
            if (course == null)
                return null;
            var courseDto = new CourseTopicsSelectListDTO
            {
                CourseId = course.Id,
                CourseName = course.Name,
                Topics = course.CourseTopics.Select(bt => new TopicSelectListDTO
                {
                    TopicId = bt.Topic.Id,
                    TopicName = bt.Topic.Name,
                }).ToList()
            };
            return courseDto;
        }
        public async Task<IEnumerable<CourseSelectListDTO>> AllCoursesSelectList()
        {
            var courses = await _courseRepository.GetAllAsync();
            return courses.Select(b => new CourseSelectListDTO
            {
                CourseId = b.Id,
                CourseName = b.Name
            });

        }

        // Course with Questions
        public async Task<GetCourseWithQuestionsDTO> GetCourseWithQuestions(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id,c=>c.Questions);
            var courseDTO = new GetCourseWithQuestionsDTO
            {
                CourseId = course.Id,
                CourseName = course.Name,
                Questions = course.Questions.Select(q => new GetQuestionDTO
                {
                    QuestionId = q.Id,
                    QuestionText = q.Text,
                }).ToList()
            };
            return courseDTO;
        }

        //Get Course Id & Name
        public async Task<CourseSelectListDTO> GetCourseSelectList(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
                return null;
            var courseDto = new CourseSelectListDTO
            {
                CourseId = course.Id,
                CourseName = course.Name
            };
            return courseDto;
        }

        public async Task<bool> CreateCourseQuestion(CourseQuestionChoicesDTO dto)
        {
            var course = await _courseRepository.GetByIdAsync(dto.CourseId, c => c.Questions);
            if (course == null)
                return false;
            var question = new Question
            {
                Text = dto.QuestionText,
                Points = dto.Points,
                Type = dto.QuestionType,
                Choices = dto.Choices.Select(c => new Choice
                {
                    Text = c.Text,
                    IsCorrect = c.IsCorrect
                }).ToList()
            };
            course.Questions.Add(question);
            await _courseRepository.UpdateAsync(course);
            return true;

            
        }

        //public async Task<CourseQuestionsSelectListDTO> CreateCourseQuestion(int id)
        //{
        //    var course = await _courseRepository.GetByIdAsync(id, c => c.Questions);
        //    if (course == null)
        //        return null;
        //    var courseDto = new CourseQuestionsSelectListDTO
        //    {
        //        CourseId = course.Id,
        //        CourseName = course.Name,
        //        Questions = course.Questions.Select(q => new QuestionSelectListDTO
        //        {
        //            QuestionId = q.Id,
        //            QuestionText = q.Text,
        //        }).ToList()
        //    };
        //    return courseDto;
        //}
    }
}

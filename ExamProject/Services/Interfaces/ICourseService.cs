using ExamProject.Models.DTOs.BranchDTOs;
using ExamProject.Models.DTOs.CoursesDTOs;
using ExamProject.Models.DTOs.TrackDTOs;

namespace ExamProject.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<IndexCourseDTO>> IndexCourses();
        Task<GetCourseDTO> GetCourse(int id);
        Task<bool> CreateCourse(CreateCourseDTO topicDto);
        Task<EditCourseDTO> EditCourse(int id);
        Task<bool> UpdateCourse(EditCourseDTO topicDto);
        Task<bool> DeleteCourse(int id);
        // Course with Questions
        Task<GetCourseWithQuestionsDTO> GetCourseWithQuestions(int id);
        Task<CourseSelectListDTO> GetCourseSelectList(int id);
        Task<bool> CreateCourseQuestion(CourseQuestionChoicesDTO dto);


        // Select List
        Task<bool> UpdateCourseTopics(int id, List<int> ToBeAdded, List<int> ToBeRemoved);

        //helpers
        Task<IEnumerable<CourseTopicsSelectListDTO>> AllCoursesWithTopicsSelectList();
        Task<CourseTopicsSelectListDTO> CourseWithTopicsSelectList(int id); // SelectList
        Task<IEnumerable<CourseSelectListDTO>> AllCoursesSelectList();

        
    }
}

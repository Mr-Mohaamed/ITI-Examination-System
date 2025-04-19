using ExamProject.Models.DTOs.BranchDTOs;
using ExamProject.Models.DTOs.CoursesDTOs;

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

        // Select List
        Task<bool> UpdateCourseTopics(int id, List<int> ToBeAdded, List<int> ToBeRemoved);

        //helpers
        Task<IEnumerable<CourseTopicsSelectListDTO>> AllCoursesWithTopicsSelectList();
        Task<CourseTopicsSelectListDTO> CourseWithTopicsSelectList(int id); // SelectList

    }
}

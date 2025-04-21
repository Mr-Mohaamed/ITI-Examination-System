using ExamProject.Models.DTOs.TrackDTOs;
using System.ComponentModel.DataAnnotations;

namespace ExamProject.Models.DTOs.StudentDTOs
{
    public record StudentCoursesDTO
    {
        [Key]
        public int StudentId { get; set; }
        [Key] 
        public string StudentName { get; set; } = string.Empty;
        [Key]
        public IEnumerable<CourseSelectListDTO> Courses { get; set; } = new List<CourseSelectListDTO>();

    }
}

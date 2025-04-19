using System.ComponentModel.DataAnnotations;

namespace ExamProject.Models.DTOs.CoursesDTOs
{
    public class CreateCourseDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(1, 1000)]
        public int Duration { get; set; } // in hours

    }
}

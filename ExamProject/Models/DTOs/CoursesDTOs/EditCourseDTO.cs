using System.ComponentModel.DataAnnotations;

namespace ExamProject.Models.DTOs.CoursesDTOs
{
    public record EditCourseDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = null!;
        [Required]
        [Range(1, 1000)]
        public int Duration { get; set; } // in hours

    }
}

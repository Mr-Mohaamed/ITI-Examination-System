using ExamProject.Models.Entities;

namespace ExamProject.Models.DTOs.ExamDTOs
{
    public record ExamSelectListDTO
    {
        public int ExamId { get; set; }
        public ExamStatus ExamStatus { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
    }
}

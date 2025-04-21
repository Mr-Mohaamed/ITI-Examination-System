using ExamProject.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace ExamProject.Models.DTOs.CoursesDTOs
{
    public record CourseQuestionChoicesDTO
    {
        [Required]
        public int CourseId { get; set; }
        [Required]
        public string CourseName { get; set; } = string.Empty;
        [Required]
        public string QuestionText { get; set; } = string.Empty;
        [Required]
        public int Points { get; set; }
        [Required]
        public QuestionType QuestionType { get; set; }
        [Required]
        public List<CreateChoiceDTO> Choices { get; set; } = new List<CreateChoiceDTO>();
    }
    public record CreateChoiceDTO
    {
        [Required]
        public string Text { get; set; } = string.Empty;
        [Required]
        public bool IsCorrect { get; set; }
    }
}

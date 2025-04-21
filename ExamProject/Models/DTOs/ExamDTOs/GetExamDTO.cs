using ExamProject.Models.DTOs.QuestionDTOs;
using ExamProject.Models.Entities;

namespace ExamProject.Models.DTOs.ExamDTOs
{
    public record GetExamDTO
    {
        public int ExamId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public ExamStatus ExamStatus { get; set; } = ExamStatus.NotStarted;
        public DateTime StartTime { get; set; }

        public List<QuestionDTO> Questions { get; set; } = new List<QuestionDTO>();
    }
}

using ExamProject.Models.DTOs.ExamDTOs;

namespace ExamProject.Models.DTOs.StudentDTOs
{
    public class StudentExamsDTO
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public List<ExamSelectListDTO> Exams { get; set; } = new List<ExamSelectListDTO>();
    }
}

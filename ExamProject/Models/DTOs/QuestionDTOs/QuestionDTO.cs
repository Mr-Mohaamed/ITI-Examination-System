using ExamProject.Models.DTOs.ChoiceDTOs;
using ExamProject.Models.Entities;

namespace ExamProject.Models.DTOs.QuestionDTOs
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public QuestionType QuestionType { get; set; }
        public int QuestionPoints { get; set; }
        public IEnumerable<ChoiceDTO> Choices { get; set; } = new List<ChoiceDTO>();
    }
}

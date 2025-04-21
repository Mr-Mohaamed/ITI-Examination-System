namespace ExamProject.Models.DTOs.QuestionDTOs
{
    public class SubmitExamAnswersDTO
    {
        public int ExamId { get; set; }
        public int StudentId { get; set; }
        public List<QuestionAnswerDTO> Questions { get; set; }
    }

}

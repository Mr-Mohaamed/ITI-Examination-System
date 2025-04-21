namespace ExamProject.Models.DTOs.CoursesDTOs
{
    public class GetCourseWithQuestionsDTO
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public List<GetQuestionDTO> Questions { get; set; } = new List<GetQuestionDTO>();
    }
    public class GetQuestionDTO
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
    }
}

namespace ExamProject.Models.Entities
{
    public class Choice
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
        // Navigation properties
        public virtual Question Question { get; set; } = null!;
        public virtual ICollection<ExamQuestion> ExamQuestions { get; set; } = new List<ExamQuestion>();
    }
}

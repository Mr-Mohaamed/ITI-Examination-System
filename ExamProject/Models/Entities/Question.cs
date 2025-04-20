namespace ExamProject.Models.Entities
{
    public enum QuestionType
    {
        MCQ,
        TrueFalse,
    }
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Points { get; set; }
        public QuestionType Type { get; set; }
        public int CourseId { get; set; }
        
        // Navigation properties
        public virtual Course Course { get; set; } = null!;
        public virtual ICollection<ExamQuestion> ExamQuestions { get; set; } = new List<ExamQuestion>();
        public virtual ICollection<Choice> Choices { get; set; } = new List<Choice>();
    }
}

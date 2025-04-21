namespace ExamProject.Models.Entities
{
    public class ExamQuestion
    {
        public int QuestionId { get; set; }
        public int ExamId { get; set; }
        public int? StudentAnswerId { get; set; }

        // Navigation properties
        public virtual Question Question { get; set; } = null!;
        public virtual Exam Exam { get; set; } = null!;
        public virtual Choice? StudentAnswer { get; set; } = null!;
    }
}

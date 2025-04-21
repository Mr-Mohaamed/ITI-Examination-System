namespace ExamProject.Models.Entities
{
    public enum ExamStatus
    {
        Passed,
        Failed,
        InProgress,
        NotStarted
    }
    public class Exam
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int StudentPoints { get; set; }
        public int TotalPoints { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public ExamStatus Status { get; set; } = ExamStatus.NotStarted;

        // Navigation properties
        public virtual Student Student { get; set; } = null!;
        public virtual Course Course { get; set; } = null!;
        public virtual ICollection<ExamQuestion> ExamQuestions { get; set; } = new HashSet<ExamQuestion>();
    }
}

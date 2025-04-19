namespace ExamProject.Models.Entities
{
    public class CourseTopic
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public int CourseId { get; set; }

        // Navigation properties
        public Topic Topic { get; set; } = null!;
        public Course Course { get; set; } = null!;

    }
}

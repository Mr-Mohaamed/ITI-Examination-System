namespace ExamProject.Models.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; } // in hours
        // Navigation properties

        // Course --- Track
        public ICollection<CourseTrack> CourseTracks { get; set; } = new List<CourseTrack>();
        // Course --- Topic
        public ICollection<CourseTopic> CourseTopics { get; set; } = new List<CourseTopic>();
        // Course --- Exam
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
        // Course --- Questions
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}

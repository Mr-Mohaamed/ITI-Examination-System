namespace ExamProject.Models.Entities
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public int CourseId { get; set; }
        //public Course Course { get; set; }
        // Navigation property for the many-to-many relationship with Course
        public ICollection<CourseTopic> CourseTopics { get; set; } = new List<CourseTopic>();
    }
}

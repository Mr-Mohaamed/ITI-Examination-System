using System.ComponentModel.DataAnnotations;

namespace ExamProject.Models.Entities
{
    public class CourseTopic
    {
        //public int Id { get; set; }
        // Composite key
        [Key]
        public int TopicId { get; set; }
        [Key]
        public int CourseId { get; set; }

        // Navigation properties
        public Topic Topic { get; set; } = null!;
        public Course Course { get; set; } = null!;

    }
}

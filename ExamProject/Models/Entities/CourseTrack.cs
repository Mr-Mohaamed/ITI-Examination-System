using System.ComponentModel.DataAnnotations;

namespace ExamProject.Models.Entities
{
    public class CourseTrack
    {
        [Key]
        public int CourseId { get; set; }
        [Key]
        public int TrackId { get; set; }
        
        // Navigation properties

        public Course Course { get; set; } = null!;
        public Track Track { get; set; } = null!;
        public ICollection<CourseAssignment> CourseAssignments { get; set; } = new List<CourseAssignment>();
    }
}

// Done
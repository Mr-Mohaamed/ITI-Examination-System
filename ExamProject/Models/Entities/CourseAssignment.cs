namespace ExamProject.Models.Entities
{
    public class CourseAssignment
    {
        //public int Id { get; set; }
        public int InstructorId { get; set; }
        public int BranchId { get; set; }
        public int TrackId { get; set; }
        public int CourseId { get; set; }

        // Navigation properties
        public Instructor Instructor { get; set; }
        public Branch Branch { get; set; }
        public CourseTrack CourseTrack { get; set; } = null!;
    }
}

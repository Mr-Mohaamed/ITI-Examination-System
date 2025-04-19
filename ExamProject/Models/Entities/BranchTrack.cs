namespace ExamProject.Models.Entities
{
    public class BranchTrack
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public int TrackId { get; set; }

        // Navigation properties

        public Branch Branch { get; set; } = null!;
        public Track Track { get; set; } = null!;
        public ICollection<Student> Students { get; set; } = new List<Student>(); // Track  --- Course
    }
}

//Done
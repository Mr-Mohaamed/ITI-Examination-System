namespace ExamProject.Models.Entities
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; } // in hours

        // Navigation properties
        public ICollection<BranchTrack> BranchTracks { get; set; } = new List<BranchTrack>(); // Branch --- Track
        public ICollection<CourseTrack> CourseTracks { get; set; } = new List<CourseTrack>(); // Track  --- Course 
    }
}

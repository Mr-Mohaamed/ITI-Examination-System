namespace ExamProject.Models.Entities
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Navigation properties

        // Branch --- Track
        public ICollection<BranchTrack> BranchTracks { get; set; } = new List<BranchTrack>(); 
        // Branch -- TrackCourse -- Instructor
        public ICollection<CourseAssignment> CourseAssignments { get; set; } = new List<CourseAssignment>(); 
    }
}

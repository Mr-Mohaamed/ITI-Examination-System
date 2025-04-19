namespace ExamProject.Models.DTOs.StudentDTOs
{
    public record IndexStudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int BranchId { get; set; }
        public string BranchName { get; set; } = null!;
        public int TrackId { get; set; }
        public string TrackName { get; set; } = null!;
    }
}

namespace ExamProject.Models.DTOs.StudentDTOs
{
    public class GetStudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int Age { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; } = null!;
        public int TrackId { get; set; }
        public string TrackName { get; set; } = null!;
    }
}

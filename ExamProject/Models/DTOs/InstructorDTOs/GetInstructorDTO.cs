namespace ExamProject.Models.DTOs.InstructorDTOs
{
    public class GetInstructorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public ICollection<GetCourseDTO> CourseInfos { get; set; } = new List<GetCourseDTO>();
    }
    public class GetCourseDTO
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public int TrackId { get; set; }
        public string TrackName { get; set; } = string.Empty;
    }
}

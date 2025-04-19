namespace ExamProject.Models.DTOs.CoursesDTOs
{
    public class GetCourseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Duration { get; set; }
        public List<GetCourseTopicDTO> Topics { get; set; } = new List<GetCourseTopicDTO>();
        public List<GetCoursesInstructorsDTO> Assignments { get; set; } = new List<GetCoursesInstructorsDTO>();

    }
    public class GetCourseTopicDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
    public class GetCoursesInstructorsDTO
    {
        public int InstructorId { get; set; }
        public string InstructorName { get; set; } = string.Empty;
        public int BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public int TrackId { get; set; }
        public string TrackName { get; set; } = string.Empty;
    }
}

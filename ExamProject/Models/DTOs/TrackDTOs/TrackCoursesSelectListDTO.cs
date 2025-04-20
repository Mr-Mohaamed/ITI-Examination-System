namespace ExamProject.Models.DTOs.TrackDTOs
{
    public record TrackCoursesSelectListDTO
    {
        public int TrackId { get; set; }
        public string TrackName { get; set; } = string.Empty;
        public List<CourseSelectListDTO> Courses { get; set; } = new List<CourseSelectListDTO>();
    }

    public record CourseSelectListDTO
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
    }
}

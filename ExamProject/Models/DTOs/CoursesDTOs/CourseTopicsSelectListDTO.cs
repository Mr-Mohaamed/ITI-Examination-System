namespace ExamProject.Models.DTOs.CoursesDTOs
{
    public class CourseTopicsSelectListDTO
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public List<TopicSelectListDTO> Topics { get; set; } = new List<TopicSelectListDTO>();
    }

    public record TopicSelectListDTO
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; } = string.Empty;
    }
}

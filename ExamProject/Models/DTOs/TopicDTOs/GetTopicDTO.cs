namespace ExamProject.Models.DTOs.TopicDTOs
{
    public record GetTopicDTO
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public IEnumerable<GetTopicCourseDTO> Courses { get; init; } = Enumerable.Empty<GetTopicCourseDTO>();
    }
    public record GetTopicCourseDTO
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}

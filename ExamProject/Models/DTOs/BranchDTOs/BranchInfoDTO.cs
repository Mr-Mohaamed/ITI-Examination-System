namespace ExamProject.Models.DTOs.BranchDTOs
{
    public record BranchInfoDTO
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public IEnumerable<TrackInfoDTO> Tracks { get; init; } = Enumerable.Empty<TrackInfoDTO>();
    }

    public record TrackInfoDTO
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public int StudentsCount { get; init; }
        public IEnumerable<StudentInfoDTO> Students { get; init; } = Enumerable.Empty<StudentInfoDTO>();
    }
    public record StudentInfoDTO
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}

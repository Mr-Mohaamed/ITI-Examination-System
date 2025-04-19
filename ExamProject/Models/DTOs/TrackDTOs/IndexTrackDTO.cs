namespace ExamProject.Models.DTOs.TrackDTOs
{
    public record IndexTrackDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Duration { get; set; } // in hours
        public int BranchesCount { get; set; }
    }
}

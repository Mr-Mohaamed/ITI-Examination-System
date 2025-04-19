namespace ExamProject.Models.DTOs.TrackDTOs
{
    public class CreateTrackDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Duration { get; set; }
    }
}

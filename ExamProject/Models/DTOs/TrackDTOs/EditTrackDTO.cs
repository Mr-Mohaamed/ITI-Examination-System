namespace ExamProject.Models.DTOs.TrackDTOs
{
    public class EditTrackDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Duration { get; set; }
    }
}

namespace ExamProject.Models.DTOs.BranchDTOs
{
    public record BranchTracksSelectListDTO
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public List<TrackSelectListDTO> Tracks { get; set; } = new List<TrackSelectListDTO>();
    }

    public record TrackSelectListDTO
    {
        public int TrackId { get; set; }
        public string TrackName { get; set; } = string.Empty;
    }
}

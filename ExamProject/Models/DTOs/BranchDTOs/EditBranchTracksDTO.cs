namespace ExamProject.Models.DTOs.BranchDTOs
{
    public class EditBranchTracksDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<EditBranchTrackInfoDTO> Tracks { get; set; } = Enumerable.Empty<EditBranchTrackInfoDTO>();
    }
    public class EditBranchTrackInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

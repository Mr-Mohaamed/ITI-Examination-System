namespace ExamProject.Models.DTOs.TrackDTOs
{
    public record GetTrackDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Duration { get; set; }
        public List<GetTrackInfoDTO> Branches { get; set; } = new List<GetTrackInfoDTO>();


    }
    public record GetTrackInfoDTO
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public int numberOfStudents { get; set; }
        public List<GetTrackInfoStudentDTO> Students { get; set; } = new List<GetTrackInfoStudentDTO>();
    }
    public record class GetTrackInfoStudentDTO
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
    }
}

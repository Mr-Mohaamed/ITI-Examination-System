namespace ExamProject.Models.DTOs.InstructorDTOs
{
    public record IndexInstructorDTO
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
    }
}

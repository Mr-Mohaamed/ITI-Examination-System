namespace ExamProject.Models.DTOs.ChoiceDTOs
{
    public class ChoiceDTO
    {
        public int Id { get; set; }
        public string ChoiceText { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}

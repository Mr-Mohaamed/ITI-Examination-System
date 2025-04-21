using System.ComponentModel.DataAnnotations;

namespace ExamProject.Models.DTOs.StudentDTOs
{
    public record StudentSelectListDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}

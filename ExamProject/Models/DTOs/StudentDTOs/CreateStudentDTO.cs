using System.ComponentModel.DataAnnotations;

namespace ExamProject.Models.DTOs.StudentDTOs
{
    public class CreateStudentDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Address { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; } = DateTime.Now;
        [Required]
        public int BranchId { get; set; }
        [Required]
        public int TrackId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ExamProject.Models.DTOs.InstructorDTOs
{
    public class EditInstructorDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string Phone { get; set; }
        [StringLength(100, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string Address { get; set; }
    }
}

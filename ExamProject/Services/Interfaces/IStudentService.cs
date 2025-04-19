using ExamProject.Models.DTOs.StudentDTOs;

namespace ExamProject.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<IndexStudentDTO>> IndexStudents();
        Task<GetStudentDTO> GetStudent(int id);
        Task<bool> CreateStudent(CreateStudentDTO branch);
        Task<EditStudentDTO> EditStudent(int id);
        Task<bool> UpdateStudent(EditStudentDTO branch);
        Task<bool> DeleteStudent(int id);
    }

}

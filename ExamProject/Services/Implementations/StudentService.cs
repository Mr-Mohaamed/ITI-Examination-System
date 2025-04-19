using ExamProject.Models.DTOs.StudentDTOs;
using ExamProject.Models.Entities;
using ExamProject.Repositories.Interfaces;
using ExamProject.Services.Interfaces;

namespace ExamProject.Services.Implementations
{
    public class StudentService(IRepository<Student> studentRepository) : IStudentService
    {
        private readonly IRepository<Student> _studentRepository = studentRepository;

        //Get : Student
        public async Task<IEnumerable<IndexStudentDTO>> IndexStudents()
        {
            var students = await _studentRepository.GetAllWithNestedIncludesAsync(["BranchTrack.Branch", "BranchTrack.Track"]);
            var studentDTOs = students.Select(s => new IndexStudentDTO
            {
                Id = s.Id,
                Name = s.Name,
                BranchId = s.BranchTrack.BranchId,
                TrackId = s.BranchTrack.TrackId,
                BranchName = s.BranchTrack.Branch.Name,
                TrackName = s.BranchTrack.Track.Name,
            });
            return studentDTOs;
        }

        //Get : Student/Details/5
        public async Task<GetStudentDTO> GetStudent(int id)
        {
            var student = await _studentRepository.GetByIdWithNestedIncludesAsync(id, ["BranchTrack.Branch", "BranchTrack.Track"]);
            if (student == null) return null;
            var studentDTO = new GetStudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Phone = student.Phone,
                Address = student.Address,
                Age = student.Age,
                BranchId = student.BranchTrack.BranchId,
                TrackId = student.BranchTrack.TrackId,
                BranchName = student.BranchTrack.Branch.Name,
                TrackName = student.BranchTrack.Track.Name,
            };
            return studentDTO;
        }


        //Post : Student/Create
        public async Task<bool> CreateStudent(CreateStudentDTO studentDto)
        {
            var student = new Student
            {
                Name = studentDto.Name,
                Email = studentDto.Email,
                Phone = studentDto.Phone,
                Address = studentDto.Address,
                DOB = studentDto.DOB,
                BranchId = studentDto.BranchId,
                TrackId = studentDto.TrackId,
            };
            try
            {
                await _studentRepository.AddAsync(student);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


        //Get : Student/Edit/5
        public async Task<EditStudentDTO> EditStudent(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null) return null;
            var studentDTO = new EditStudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Phone = student.Phone,
                Address = student.Address,
                DOB = student.DOB,
                BranchId = student.BranchId,
                TrackId = student.TrackId,
            };
            return studentDTO;
        }
        //Put : Student/Edit/5
        public async Task<bool> UpdateStudent(EditStudentDTO student)
        {
            var studentToUpdate = await _studentRepository.GetByIdAsync(student.Id);
            if (studentToUpdate == null)
                return false;
            studentToUpdate.Name = student.Name;
            studentToUpdate.Email = student.Email;
            studentToUpdate.Phone = student.Phone;
            studentToUpdate.Address = student.Address;
            studentToUpdate.DOB = student.DOB;
            studentToUpdate.BranchId = student.BranchId;
            studentToUpdate.TrackId = student.TrackId;
            try
            {
                await _studentRepository.UpdateAsync(studentToUpdate);
            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        //Delete : Student/Delete/5
        public async Task<bool> DeleteStudent(int id)
        {
            try
            {
                await _studentRepository.DeleteAsync(id);
            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}

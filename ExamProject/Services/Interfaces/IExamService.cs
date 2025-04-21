using ExamProject.Models.DTOs.ExamDTOs;
using ExamProject.Models.DTOs.ExamDTOs;
using ExamProject.Models.DTOs.QuestionDTOs;

namespace ExamProject.Services.Interfaces
{
    public interface IExamService
    {
        //Task<IEnumerable<IndexExamDTO>> IndexExams();
        Task<GetExamDTO> GetExam(int id);
        //Task<bool> CreateExam(CreateExamDTO topicDto);
        //Task<EditExamDTO> EditExam(int id);
        //Task<bool> UpdateExam(EditExamDTO topicDto);
        Task<bool> SubmitExam(SubmitExamAnswersDTO model);
        Task<bool> DeleteExam(int id);
        Task<bool> GenerateExam(int studentId, int courseId);
    }
}

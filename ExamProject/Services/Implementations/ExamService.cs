using ExamProject.Models.DTOs.ChoiceDTOs;
using ExamProject.Models.DTOs.ExamDTOs;
using ExamProject.Models.DTOs.QuestionDTOs;
using ExamProject.Models.DTOs.ExamDTOs;
using ExamProject.Models.Entities;
using ExamProject.Services.Interfaces;

namespace ExamProject.Services.Implementations
{
    public class ExamService(
        IRepository<Exam> examRepository,
        IRepository<Question> questionRepository,
        IRepository<Student> studentRepository,
        IRepository<Course> courseRepository
        ) : IExamService
    {
        private readonly IRepository<Exam> _examRepository = examRepository;
        private readonly IRepository<Question> _questionRepository = questionRepository;
        private readonly IRepository<Student> _studentRepository = studentRepository;
        private readonly IRepository<Course> _courseRepository = courseRepository;

        //public Task<IEnumerable<IndexExamDTO>> IndexExams()
        //{
        //    throw new NotImplementedException();
        //}
        public async Task<GetExamDTO> GetExam(int id)
        {
            // Get the exam by ID
            var exam = await _examRepository.GetByIdWithNestedIncludesAsync(id, ["ExamQuestions.Question.Choices", "Student", "Course"]);
            if (exam == null)
                throw new Exception("Exam not found");
            // Check Exam Status
            if (exam.Status == ExamStatus.NotStarted)
            {
                exam.Status = ExamStatus.InProgress;
                exam.Date = DateTime.Now;
                await _examRepository.UpdateAsync(exam);
            }
            // Get the student ID and course ID
            var studentId = exam.StudentId;
            var courseId = exam.CourseId;
            if (exam.ExamQuestions == null)
                throw new Exception("No questions found for this exam");
            if (exam.ExamQuestions.Count() < 10)
                throw new Exception("Not enough questions for this exam");
            // Get the questions for the exam
            var questions = exam.ExamQuestions.Select(eq => new QuestionDTO
            {
                Id = eq.Question.Id,
                QuestionText = eq.Question.Text,
                QuestionPoints = eq.Question.Points,
                QuestionType = eq.Question.Type,
                StudentAnswer = eq.StudentAnswerId ?? 0,
                Choices = eq.Question.Choices.Select(c => new ChoiceDTO
                {
                    Id = c.Id,
                    ChoiceText = c.Text,
                    IsCorrect = c.IsCorrect
                }).ToList()
            }).ToList();

            // Create the DTO to return

            var examDTO = new GetExamDTO
            {
                ExamId = id,
                StudentId = studentId,
                CourseId = courseId,
                ExamStatus = exam.Status,
                CourseName = exam.Course.Name,
                StudentName = exam.Student.Name,
                StartTime = exam.Date,
                Questions = questions
            };
            return examDTO;
        }
        public async Task<bool> SubmitExam(SubmitExamAnswersDTO model)
        {
            // Check if the exam exists
            var exam = await _examRepository.GetByIdWithNestedIncludesAsync(model.ExamId, ["Course", "ExamQuestions.Question.Choices"]);
            if (exam == null)
                throw new Exception("Exam not found");
            //Update ExamQuestions
            foreach (var question in model.Questions)
            {
                // Check if the Answer is valid
                if (question.Answer == 0)
                    continue;
                var examQuestion = exam.ExamQuestions.FirstOrDefault(eq => eq.QuestionId == question.Id);
                if (examQuestion != null)
                {
                    examQuestion.StudentAnswerId = question.Answer;
                }
            }
            // Update the exam status
            //exam.Status = ExamStatus.Submitted;
            // Save the changes to the database
            try
            {
                //await _examRepository.UpdateAsync(exam);
                //var examPoints = exam.ExamQuestions.Sum(q => q.Question.Points);
                var examPoints = exam.TotalPoints;
                var studentPoints = 0;
                // Calculate the studentPoints
                foreach (var question in exam.ExamQuestions)
                {
                    var questionPoints = question.Question.Points;
                    var studentAnswer = question.StudentAnswerId;
                    if (studentAnswer == null)
                        continue;
                    var correctAnswer = question.Question.Choices.FirstOrDefault(c => c.IsCorrect)?.Id;
                    if (studentAnswer == correctAnswer)
                    {
                        studentPoints += questionPoints;
                    }
                }
                // Calculate the percentage
                var percentage = (double)studentPoints / examPoints * 100;
                // Update the exam status
                // if percentage >= 50
                if (percentage >= 50)
                {
                    exam.Status = ExamStatus.Passed;
                }
                else
                {
                    exam.Status = ExamStatus.Failed;
                }

                // Update the exam points
                exam.TotalPoints = examPoints;
                // Update the student Points
                exam.StudentPoints = studentPoints;
                // Save the changes to the database
                await _examRepository.UpdateAsync(exam);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to submit exam", ex);
                //return false;
            }
            return true;
        }

        //public Task<bool> CreateExam(CreateExamDTO topicDto)
        //{
        //    throw new NotImplementedException();
        //}


        //public Task<EditExamDTO> EditExam(int id)
        //{
        //    throw new NotImplementedException();
        //}
        //public Task<bool> UpdateExam(EditExamDTO topicDto)
        //{
        //    throw new NotImplementedException();
        //}
        public async Task<bool> DeleteExam(int id)
        {
            // Check if the exam exists
            var exam = await _examRepository.GetByIdAsync(id);
            if (exam == null)
                throw new Exception("Exam not found");
            // Delete the exam
            try
            {
                _examRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete exam", ex);
            }
            return true;
        }

        public async Task<bool> GenerateExam(int studentId, int courseId)
        {
            // Check if the student exists
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
                throw new Exception("Student not found");
            // Check if the course exists
            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null)
                throw new Exception("Course not found");

            // Get all questions for the course with nested includes
            var questions = await _questionRepository.GetAllWithNestedIncludesAndFilterAsync(q => q.CourseId == courseId, "Choices");
            if (questions == null)
                throw new Exception("No questions found for this course");
            if (questions.Count() < 10)
                throw new Exception("Not enough questions for this course");
            // Randomly select 10 questions
            var random = new Random();
            var selectedQuestions = questions.OrderBy(q => random.Next()).Take(10);

            // Save the exam to the database
            var exam = new Exam
            {
                StudentId = studentId,
                CourseId = courseId,
            };
            await _examRepository.AddAsync(exam);
            if (exam.Id == 0)
                return false;
            // Save the questions to the database
            foreach (var question in selectedQuestions)
            {
                var examQuestion = new ExamQuestion
                {
                    ExamId = exam.Id,
                    QuestionId = question.Id,
                    Exam = exam,
                    Question = question
                };
                exam.ExamQuestions.Add(examQuestion);
            }
            try
            {
                var examPoints = exam.ExamQuestions.Sum(q => q.Question.Points);
                // Update the exam points
                exam.TotalPoints = examPoints;
                await _examRepository.UpdateAsync(exam);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save exam", ex);
            }

            //// Create the DTO to return

            //var questionsDTO = selectedQuestions.Select(q => new QuestionDTO
            //{
            //    Id = q.Id,
            //    QuestionText = q.Text,
            //    QuestionPoints = q.Points,
            //    QuestionType = q.Type,
            //    Choices = q.Choices.Select(c => new ChoiceDTO
            //    {
            //        Id = c.Id,
            //        ChoiceText = c.Text,
            //        IsCorrect = c.IsCorrect
            //    }).ToList()
            //});
            //// Create the DTO to return

            //var examDTO = new GeneratedExamDTO
            //{
            //    StudentId = studentId,
            //    CourseId = courseId,
            //    Questions = questionsDTO
            //};
            return true;
        }
    }
}

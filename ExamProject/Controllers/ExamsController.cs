using ExamProject.Models.DTOs.ExamDTOs;
using ExamProject.Models.DTOs.QuestionDTOs;
using ExamProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExamProject.Controllers
{
    [Route("[controller]")]
    public class ExamsController(IExamService examService) : Controller
    {
        private readonly IExamService _examService = examService;
        // GET: Exam/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var exam = await _examService.GetExam(id);
            if (exam == null)
                return NotFound();
            return View(exam);
        }
        [HttpPost]
        public async Task<IActionResult> SubmitAnswers(SubmitExamAnswersDTO model)
        {
            if (!ModelState.IsValid)
            {
                // Maybe re-fetch the exam and return the view
                return View("ExamDetails", model);
            }
            var result = await _examService.SubmitExam(model);
            if (!result)
            {
                ModelState.AddModelError("", "Failed to submit exam");
                return View("ExamDetails", model);
            }



            // Redirect to result page or confirmation
            return RedirectToAction("Index", "Students");
        }
    }
}

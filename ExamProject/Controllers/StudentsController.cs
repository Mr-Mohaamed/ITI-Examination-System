using ExamProject.Models.DTOs.StudentDTOs;
using ExamProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExamProject.Controllers
{
    [Route("[controller]")]
    public class StudentsController(
            IStudentService studentService,
            IBranchService branchService,
            ITrackService TrackService,
            IExamService examService
        ) : Controller
    {
        private readonly IStudentService _studentService = studentService;
        private readonly IBranchService _branchService = branchService;
        private readonly ITrackService _trackService = TrackService;
        private readonly IExamService _examService = examService;

        // GET: Student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IndexStudentDTO>>> Index()
        {
            var students = await _studentService.IndexStudents();
            return View(students);
        }

        // GET: api/Student/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetStudentDTO>> Details(int id)
        {
            var student = await _studentService.GetStudent(id);
            if (student == null)
                return NotFound();

            return View(student);
        }
        //Get: Student/Create
        [HttpGet("Create")]
        public async Task<ActionResult> Create()
        {
            // Put Branches and tracks in the ViewBag for the dropdowns
            ViewBag.Branches = await _branchService.AllBranchesWithTrackForSelectList();
            return View();
        }

        // POST: Student
        [HttpPost("Create")]
        //public async Task<ActionResult> Create([FromBody] CreateStudentDTO dto)
        public async Task<ActionResult> Create(CreateStudentDTO dto)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Create));

            var result = await _studentService.CreateStudent(dto);
            if (result == null)
                return BadRequest("Failed to create student");
            return RedirectToAction(nameof(Index));
        }

        // GET: Student/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            var studentDTO = await _studentService.EditStudent(id);
            if (studentDTO == null)
                return NotFound();

            // Put Branches and tracks in the ViewBag for the dropdowns
            ViewBag.Branches = await _branchService.AllBranchesWithTrackForSelectList();

            return View(studentDTO);
        }
        // Post: /Student/Edit/5
        [HttpPost("Edit/{id}")]
        //public async Task<ActionResult> Update(int id, [FromBody] EditStudentDTO dto)
        public async Task<ActionResult> Update(int id, EditStudentDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _studentService.UpdateStudent(dto);
            if (result == null)
                return BadRequest("Failed to update student");
            return RedirectToAction(nameof(Index));
        }

        // DELETE: api/Student/5
        [HttpPost("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _studentService.DeleteStudent(id);
                if (result == null)
                    return NotFound("Student not found");
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // GET: Student/{id}/Exams/Create
        [HttpGet("{id}/Exams/Create")]
        public async Task<ActionResult> CreateExam(int id)
        {
            // student courses
            var student = await _studentService.GetStudentWithCourses(id);
            if (student == null)
                return NotFound();
            return View(student);
        }

        // GET: Student/{id}/Exams
        [HttpGet("{id}/Exams")]
        public async Task<ActionResult> GetExams(int id)
        {
            var student = await _studentService.GetStudentWithExams(id);
            if (student == null)
                return NotFound();
            return View(student);
        }

        // Post: Student/{id}/Exams/Create
        [HttpPost("{id}/Exams/Create")]
        public async Task<ActionResult> CreateExam(int studentId, int courseId)
        {
            // student courses
            var student = await _studentService.GetStudentSelectListAsync(studentId);
            if (student == null)
                return NotFound();
            var result = await _examService.GenerateExam(studentId, courseId);
            return RedirectToAction(nameof(GetExams), new {id = studentId});
        }
    }
}


//HTTP   | URL                   | Action Method    | Description
//GET    | /Students             | GetAll()         | Returns a view with all students
//GET    | /Students/{id}        | GetById(int id)  | Returns details view for a specific student by ID
//GET    | /Students/Create      | Create()         | Returns the form view to create a student
//POST   | /Students/Create      | Create(dto)      | Submits the create form (form-based post)
//GET    | /Students/Edit/{id}   | Edit(int id)     | Returns the edit form view
//POST   | /Students/Edit/{id}   | Update(id, dto)  | Submits the edit form with updates
//POST   | /Students/Delete/{id} | Delete(id)       | Deletes a student(form or JS-based)
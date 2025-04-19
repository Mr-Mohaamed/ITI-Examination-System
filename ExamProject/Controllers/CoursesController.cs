using ExamProject.Models.DTOs.BranchDTOs;
using ExamProject.Models.DTOs.CoursesDTOs;
using ExamProject.Models.DTOs.TopicDTOs;
using ExamProject.Services.Implementations;
using ExamProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExamProject.Controllers
{
    [Route("[controller]")]
    public class CoursesController(ICourseService courseService, ITopicService topicService) : Controller
    {
        private readonly ITopicService _topicService = topicService;
        ICourseService _courseService = courseService;
        // GET: Course
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IndexTopicDTO>>> Index()
        {
            var courses = await _courseService.IndexCourses();
            return View(courses);
        }

        // GET: Course/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCourseDTO>> Details(int id)
        {
            var course = await _courseService.GetCourse(id);
            return View(course);
        }

        //Get: Course/Create
        [HttpGet("Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Course
        [HttpPost("Create")]
        public async Task<ActionResult> Create(CreateCourseDTO dto)
        {
            if (dto == null)
            {
                return View();
            }
            var result = await _courseService.CreateCourse(dto);
            if (result == null)
            {
                return BadRequest("Failed to create course");
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Course/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            var course = await _courseService.EditCourse(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        // Post: /Course/Edit/5
        [HttpPost("Edit/{id}")]
        public async Task<ActionResult> Edit(int id, EditCourseDTO dto) 
        {
            if (dto == null)
            {
                return View();
            }
            if (ModelState.IsValid)
            {
                var update = await _courseService.UpdateCourse(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        // DELETE: Course/5
        [HttpPost("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var course = await _courseService.DeleteCourse(id);
            if (course == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Course/5/Tracks
        [HttpGet("{id}/Topics")]
        public async Task<ActionResult<CourseTopicsSelectListDTO>> EditCourseTopics(int id)
        {
            var topics = await _topicService.TopicsSelectList();
            ViewBag.Topics = topics;
            var course = await _courseService.CourseWithTopicsSelectList(id);
            if (course == null)
                return NotFound();

            return View(course);
        }

        // Post: Course/5/Topics
        [HttpPost("{id}/Topics")]
        public async Task<ActionResult<CourseTopicsSelectListDTO>> UpdateCourseTopics(int id, List<int> ToBeAdded, List<int> ToBeRemoved)
        {
            if (ToBeAdded == null && ToBeRemoved == null)
                return BadRequest("No tracks to add or remove");
            var course = await _courseService.GetCourse(id);
            if (course == null)
                return NotFound();
            await _courseService.UpdateCourseTopics(id, ToBeAdded, ToBeRemoved);
            return RedirectToAction(nameof(Index));
        }
    }
}

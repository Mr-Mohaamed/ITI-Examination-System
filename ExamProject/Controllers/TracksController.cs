using ExamProject.Models.DTOs.TrackDTOs;
using ExamProject.Services.Implementations;
using ExamProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;
using System.Buffers.Text;
using ExamProject.Models.DTOs.BranchDTOs;

namespace ExamProject.Controllers
{
    [Route("[controller]")]
    public class TracksController(
            ITrackService TrackService,
            ICourseService courseService
        ) : Controller
    {
        private readonly ITrackService _trackService = TrackService;
        private readonly ICourseService _courseService = courseService;

        // GET: Track
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IndexTrackDTO>>> Index()
        {
            var tracks = await _trackService.IndexTrackes();
            return View(tracks);
        }

        // GET: api/Track/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetTrackDTO>> Details(int id)
        {
            var track = await _trackService.GetTrack(id);
            if (track == null)
                return NotFound();

            return View(track);
        }
        //Get: Track/Create
        [HttpGet("Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Track
        [HttpPost("Create")]
        //public async Task<ActionResult> Create([FromBody] CreateTrackDTO dto)
        public async Task<ActionResult> Create(CreateTrackDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _trackService.CreateTrack(dto);
            if (result == null)
                return BadRequest("Failed to create track");
            //return CreatedAtAction(nameof(GetById), new { id = trackId }, dto);
            return RedirectToAction(nameof(Index));
        }

        // GET: Track/Edit/5
        [HttpGet("{id}/Edit")]
        public async Task<ActionResult> Edit(int id)
        {
            var trackDTO = await _trackService.EditTrack(id);
            if (trackDTO == null)
                return NotFound();

            return View(trackDTO);
        }
        // Post: /Track/Edit/5
        [HttpPost("{id}/Edit")]
        //public async Task<ActionResult> Update(int id, [FromBody] EditTrackDTO dto)
        public async Task<ActionResult> Update(int id, EditTrackDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _trackService.UpdateTrack(dto);
            if (!result)
                return NotFound("Track not found");
            return RedirectToAction(nameof(Details), new {id = id});
        }

        // DELETE: api/Track/5
        [HttpPost("{id}/Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _trackService.DeleteTrack(id);
                if (!result)
                    return NotFound("Track not found");
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        // New 

        // GET: Track/5/Courses
        [HttpGet("{id}/Courses")]
        public async Task<ActionResult<TrackCoursesSelectListDTO>> EditTrackCourses(int id)
        {
            var courses = await _courseService.AllCoursesSelectList();
            ViewBag.Courses = courses;
            var track = await _trackService.TrackWithCoursesSelectList(id);
            if (track == null)
                return NotFound();

            return View(track);
        }

        // Post: Track/5/Courses
        [HttpPost("{id}/Courses")]
        public async Task<ActionResult<TrackCoursesSelectListDTO>> UpdateTrackCourses(int id, List<int> ToBeAdded, List<int> ToBeRemoved)
        {
            if (ToBeAdded == null && ToBeRemoved == null)
                return BadRequest("No tracks to add or remove");
            var track = await _trackService.GetTrack(id);
            if (track == null)
                return NotFound();
            await _trackService.UpdateTrackCourses(id, ToBeAdded, ToBeRemoved);
            return RedirectToAction(nameof(Index));
        }
    }
}


//HTTP   | URL                   | Action Method    | Description
//GET    | /Tracks             | GetAll()         | Returns a view with all 
//GET    | /Tracks/{id}        | GetById(int id)  | Returns details view for a specific track by ID
//GET    | /Tracks/Create      | Create()         | Returns the form view to create a track
//POST   | /Tracks/Create      | Create(dto)      | Submits the create form (form-based post)
//GET    | /Tracks/{id}/Edit   | Edit(int id)     | Returns the edit form view
//POST   | /Tracks/{id}/Edit   | Update(id, dto)  | Submits the edit form with updates
//POST   | /Tracks/{id}/Delete | Delete(id)       | Deletes a track(form or JS-based)
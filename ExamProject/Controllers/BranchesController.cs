using ExamProject.Models.DTOs.BranchDTOs;
using ExamProject.Services.Implementations;
using ExamProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;
using System.Buffers.Text;
using ExamProject.Models.Entities;
using ExamProject.Models.DTOs.TrackDTOs;

namespace ExamProject.Controllers
{
    [Route("[controller]")]
    public class BranchesController(
            IBranchService branchService,
            ITrackService TrackService
        ) : Controller
    {
        private readonly IBranchService _branchService = branchService;
        private readonly ITrackService _trackService = TrackService;

        // GET: Branch
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IndexBranchDTO>>> Index()
        {
            var branches = await _branchService.IndexBranches();
            return View(branches);
        }

        // GET: Branch/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BranchInfoDTO>> Details(int id)
        {
            var branch = await _branchService.GetBranch(id);
            if (branch == null)
                return NotFound();

            return View(branch);
        }


        //Get: Branch/Create
        [HttpGet("Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Branch
        [HttpPost("Create")]
        public async Task<ActionResult> Create(CreateBranchDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _branchService.CreateBranch(dto);
            if (result == null)
                return BadRequest("Failed to create branch");
            return RedirectToAction(nameof(Index));
        }

        // GET: Branch/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            var branchDTO = await _branchService.EditBranch(id);
            if (branchDTO == null)
                return NotFound();

            // Put Branches and tracks in the ViewBag for the dropdowns
            ViewBag.Branches = await _branchService.AllBranchesWithTrackForSelectList();

            return View(branchDTO);
        }
        // Post: /Branch/Edit/5
        [HttpPost("Edit/{id}")]
        //public async Task<ActionResult> Update(int id, [FromBody] EditBranchDTO dto)
        public async Task<ActionResult> Update(int id, EditBranchDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _branchService.UpdateBranch(dto);
            if (result == null)
                return NotFound("Branch not found");

            return RedirectToAction(nameof(Index));
        }

        // DELETE: Branch/5
        [HttpPost("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _branchService.DeleteBranch(id);
                if (result == null)
                    return NotFound("Branch not found");
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // GET: Branch/5/Tracks
        [HttpGet("{id}/Tracks")]
        public async Task<ActionResult<TrackCoursesSelectListDTO>> EditBranchTracks(int id)
        {
            var tracks = await _trackService.TracksSelectList();
            ViewBag.Tracks = tracks;
            var branch = await _branchService.BranchWithTrackSelectList(id);
            if (branch == null)
                return NotFound();

            return View(branch);
        }

        // Post: Branch/5/Tracks
        [HttpPost("{id}/Tracks")]
        public async Task<ActionResult<TrackCoursesSelectListDTO>> UpdateBranchTracks(int id, List<int> ToBeAdded, List<int> ToBeRemoved)
        {
            if (ToBeAdded == null && ToBeRemoved == null)
                return BadRequest("No tracks to add or remove");
            var branch = await _branchService.GetBranch(id);
            if (branch == null)
                return NotFound();
            await _branchService.UpdateBranchTracks(id, ToBeAdded, ToBeRemoved);
            return RedirectToAction(nameof(Index));
        }
    }
}


//HTTP   | URL                   | Action Method    | Description
//GET    | /Branches             | GetAll()         | Returns a view with all 
//GET    | /Branches/{id}        | GetById(int id)  | Returns details view for a specific branch by ID
//GET    | /Branches/Create      | Create()         | Returns the form view to create a branch
//POST   | /Branches/Create      | Create(dto)      | Submits the create form (form-based post)
//GET    | /Branches/Edit/{id}   | Edit(int id)     | Returns the edit form view
//POST   | /Branches/Edit/{id}   | Update(id, dto)  | Submits the edit form with updates
//POST   | /Branches/Delete/{id} | Delete(id)       | Deletes a branch(form or JS-based)
using ExamProject.Models.DTOs.TopicDTOs;
using ExamProject.Models.Entities;
using ExamProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExamProject.Controllers
{
    public class TopicsController:Controller
    {
        private readonly ITopicService topicService;

        public TopicsController(ITopicService _topicService)
        {
            topicService = _topicService;
        }

        // GET: Topics (Index)
        public async Task<IActionResult> Index()
        {
            var topics = await topicService.IndexTopics();
            return View(topics);
        }

        // GET: Topics/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var topic = await topicService.GetTopic(id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        // GET: Topics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Topics/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTopicDTO topic)
        {
            if (ModelState.IsValid)
            {
                await topicService.CreateTopic(topic);
                return RedirectToAction(nameof(Index));
            }
            return View(topic);
        }

        // GET: Topics/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var topic = await topicService.EditTopic(id);

            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        // POST: Topics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditTopicDTO topic)
        {
            if (id != topic.Id)
            {
                return View(topic);
            }

            if (ModelState.IsValid)
            {
                await topicService.UpdateTopic(topic);
                return RedirectToAction(nameof(Index));
            }
            return View(topic);
        }

        //// GET: Topics/Delete/5
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var topic = await _repository.GetByIdAsync(id, t => t.CourseTopics);

        //    if (topic == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(topic);
        //}

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var topic = await topicService.DeleteTopic(id);
            if (topic == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

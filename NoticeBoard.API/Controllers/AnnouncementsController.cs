using Microsoft.AspNetCore.Mvc;
using NoticeBoard.API.Models;
using NoticeBoard.API.Repositories;

namespace NoticeBoard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementRepository _repo;

        public AnnouncementsController(IAnnouncementRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? category = null, [FromQuery] string? subCategory = null)
        {
            var announcements = await _repo.GetAllAsync();

            if (!string.IsNullOrEmpty(category))
            {
                announcements = announcements.Where(a => a.Category == category);
            }
            if (!string.IsNullOrEmpty(subCategory))
            {
                announcements = announcements.Where(a => a.SubCategory == subCategory);
            }

            return Ok(announcements);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _repo.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Announcement announcement)
        {
            await _repo.CreateAsync(announcement);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Announcement announcement)
        {
            if (id != announcement.Id) return BadRequest();

            var updated = await _repo.UpdateAsync(announcement);
            return updated ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            return deleted ? Ok() : NotFound();
        }
    }
}
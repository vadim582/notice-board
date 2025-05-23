using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoticeBoard.Web.Models;
using System.Net.Http.Json;

namespace NoticeBoard.Web.Controllers
{
    public class AnnouncementsController : Controller
    {
        private readonly HttpClient _httpClient;

        public AnnouncementsController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }
        public async Task<IActionResult> Index(string? category, string? subCategory)
        {
            ViewData["CurrentCategory"] = category;
            ViewData["CurrentSubCategory"] = subCategory;

            var query = "announcements";

            if (!string.IsNullOrEmpty(category) || !string.IsNullOrEmpty(subCategory))
            {
                var queryParams = new List<string>();
                if (!string.IsNullOrEmpty(category))
                    queryParams.Add($"category={Uri.EscapeDataString(category)}");
                if (!string.IsNullOrEmpty(subCategory))
                    queryParams.Add($"subCategory={Uri.EscapeDataString(subCategory)}");

                query += "?" + string.Join("&", queryParams);
            }

            var announcements = await _httpClient.GetFromJsonAsync<List<Announcement>>(query);

            return View(announcements);
        }
        public async Task<IActionResult> Details(int id)
        {
            var announcement = await _httpClient.GetFromJsonAsync<Announcement>($"announcements/{id}");
            if (announcement == null)
            {
                return NotFound();
            }
            return View(announcement);
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Announcement announcement)
        {
            if (!ModelState.IsValid)
                return View(announcement);

            await _httpClient.PostAsJsonAsync("announcements", announcement);
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Announcement>($"announcements/{id}"); 
            if (model == null) 
                return NotFound();
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(Announcement model)
        {
            var response = await _httpClient.PutAsJsonAsync($"announcements/{model.Id}", model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Announcement>($"announcements/{id}");
            return View(model);
        }
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _httpClient.DeleteAsync($"announcements/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
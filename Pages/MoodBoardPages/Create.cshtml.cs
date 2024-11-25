using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel;
using System.Text.Json;
using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Pages.MoodBoardPages
{
    public class CreateModel : PageModel
    {
        public IPrimaryTagService _primaryTagService { get; set; }
        [BindProperty]
        public Moodboard Moodboard { get; set; }

        [BindProperty]
        public ContentContainer Container { get; set; }

        public List<PrimaryTag> PrimaryTags { get; set; }
        public PrimaryTag PrimaryTag { get; set; }

        public CreateModel(IPrimaryTagService primaryTagService)
        {
            _primaryTagService = primaryTagService;
        }
        public void OnGet()
        {
            PrimaryTags = _primaryTagService.GetPrimaryTags();
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0; //userId bliver 0 i stedet for null hvis den ikke kan findes
            // Initialize an empty moodboard instance
            Moodboard = new Moodboard
            {
                ContentContainers = new List<ContentContainer>(),
                UserId = userId
            };
        }

        // Redirect to the Edit page with the moodboard's metadata
        public IActionResult OnPost([FromBody] Moodboard moodboard)
        {
            // Store data in session or pass via query parameters
            TempData["MoodboardData"] = JsonSerializer.Serialize(moodboard); //Gemmer moodboard i tempdata som kan læses af Edit siden. 
            return RedirectToPage("/MoodBoardPages/Edit");
        }

        public IActionResult OnPostSetTitle(string title)
        {
            Moodboard.Title = title;
            return RedirectToPage();
        }

        public IActionResult OnPostCreateContainer()
         {
            ContentContainer container = new ContentContainer()
            {
                MoodboardId = Moodboard.MoodboardId
            };
            Moodboard.ContentContainers.Add(container);
            return RedirectToPage();
         }

    }
}

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
            Moodboard = new Moodboard
            {
                UserId = userId
            };
        }

        public IActionResult OnPost()
        {
            Moodboard.PrimaryTag = PrimaryTag;
            TempData["MoodboardData"] = JsonSerializer.Serialize(Moodboard); //Gemmer moodboard i tempdata som kan læses af Edit siden. 
            return RedirectToPage("/MoodBoardPages/Edit");
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

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
        public IMoodboardService moodboardService { get; set; }

        [BindProperty]
        public Moodboard Moodboard { get; set; }

        [BindProperty]

        public List<PrimaryTag> PrimaryTags { get; set; }
        [BindProperty]
        public int PrimaryTag { get; set; }

        public int searchId { get; set; }

        public CreateModel(IPrimaryTagService primaryTagService, IMoodboardService service)
        {
            _primaryTagService = primaryTagService;
            moodboardService = service;
        }
        public void OnGet()
        {
            Moodboard = new Moodboard();
            PrimaryTags = _primaryTagService.GetPrimaryTags();
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0; //userId bliver 0 i stedet for null hvis den ikke kan findes
            Moodboard.UserId = userId;
            this.Moodboard.PrimaryTagId = 5;
            this.Moodboard.DateOfCreation = DateTime.Now;

        }

        public void OnPost()
        {
            Moodboard.PrimaryTagId = PrimaryTag;
            moodboardService.CreateMoodboard(Moodboard);
            searchId = Moodboard.MoodboardId;
            /*TempData["MoodboardData"] = JsonSerializer.Serialize(Moodboard); *///Gemmer moodboard i tempdata som kan læses af Edit siden. 
            //return RedirectToPage("/MoodBoardPages/Edit");
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

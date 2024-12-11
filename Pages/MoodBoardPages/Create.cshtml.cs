using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Pages.MoodBoardPages
{
    public class CreateModel : PageModel
    {
        public IPrimaryTagService _primaryTagService { get; set; }
        public IMoodboardService moodboardService { get; set; }
        public ISubTagService _subtagService { get; set; }
        public IContentContainerService _contentContainerService { get; set; }

        [BindProperty]
        public Moodboard Moodboard { get; set; } = new Moodboard();

        [BindProperty]
        public List<PrimaryTag> PrimaryTags { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please select a primary tag.")]
        public int PrimaryTag { get; set; }

        [BindProperty]
        public List<string> Subtags { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter at least one subtag.")]
        public string subtaginput { get; set; }

        [BindProperty]
        public List<string> ContainerType { get; set; }

        [BindProperty]
        public List<string> ContainerText { get; set; }

        public CreateModel(IPrimaryTagService primaryTagService, IMoodboardService service, IContentContainerService contentContainerService, ISubTagService subTagService)
        {
            _primaryTagService = primaryTagService;
            moodboardService = service;
            _contentContainerService = contentContainerService;
            _subtagService = subTagService;
        }

        public void OnGet()
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0; // userId becomes 0 instead of null if not found
            Moodboard.UserId = userId;

            PrimaryTags = _primaryTagService.GetPrimaryTags();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Moodboard.PrimaryTagId = PrimaryTag;
            moodboardService.CreateMoodboard(Moodboard);
            _subtagService.SplitSubtagInput(subtaginput, Moodboard.PrimaryTagId, Moodboard.MoodboardId);

            _contentContainerService.AddContainersToMoodboard(ContainerType, ContainerText, Moodboard);
            moodboardService.UpdateMoodboard(Moodboard);

            return RedirectToPage("/MoodBoardPages/Edit", new { Id = Moodboard.MoodboardId });
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

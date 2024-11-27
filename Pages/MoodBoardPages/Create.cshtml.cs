using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json.Linq;
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

        public IContentContainerService _contentContainerService { get; set; }

        [BindProperty]
        public Moodboard Moodboard { get; set; } = new Moodboard();

        [BindProperty]

        public List<PrimaryTag> PrimaryTags { get; set; }
        [BindProperty]
        public int PrimaryTag { get; set; }

        public int searchId { get; set; }

        [BindProperty]
        public List<string> ContainerType { get; set; }
        [BindProperty]
        public List<string> ContainerText { get; set; }

        public CreateModel(IPrimaryTagService primaryTagService, IMoodboardService service, IContentContainerService contentContainerService)
        {
            _primaryTagService = primaryTagService;
            moodboardService = service;
            _contentContainerService = contentContainerService;
        }
        public void OnGet()
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0; //userId bliver 0 i stedet for null hvis den ikke kan findes
            Moodboard.UserId = userId;
            
            PrimaryTags = _primaryTagService.GetPrimaryTags();

        }

        public IActionResult OnPost()
        {
            Moodboard.PrimaryTagId = PrimaryTag;
            moodboardService.CreateMoodboard(Moodboard);
            //if (Moodboard.UserId == 0)
            //{
            //    int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            //    Moodboard.UserId = userId;
            //}

            for (int i = 0; i < ContainerType.Count; i++)
            {
                var container = new ContentContainer
                {
                    Type = ContainerType[i],
                    Link = ContainerText[i],
                    MoodboardId = Moodboard.MoodboardId
                    
                };
                _contentContainerService.CreateContainer(container, Moodboard.MoodboardId);
                Moodboard.ContentContainers.Add(container);
            }
            moodboardService.UpdateMoodboard(Moodboard);




            /*TempData["MoodboardData"] = JsonSerializer.Serialize(Moodboard); *///Gemmer moodboard i tempdata som kan læses af Edit siden. 
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

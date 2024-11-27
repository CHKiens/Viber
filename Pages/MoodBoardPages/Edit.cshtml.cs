using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Pages.MoodBoardPages {
    public class EditModel : PageModel {
        IMoodboardService _moodboardService;
        IContentContainerService _contentContainerService;


        public EditModel(IMoodboardService moodboardService, IContentContainerService contentContainerService)
        {
            _moodboardService = moodboardService;
            _contentContainerService = contentContainerService;
        }
        [BindProperty]
        public Moodboard MoodBoard { get; set; }


        public void OnGet(int Id)
        {
            MoodBoard = _moodboardService.GetMoodboard(Id);
            MoodBoard.ContentContainers = _contentContainerService.GetContainers(MoodBoard.MoodboardId);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Pages.MoodBoardPages {
    public class EditModel : PageModel {
        IMoodboardService _moodboardService;

        public EditModel(IMoodboardService moodboardService)
        {
            _moodboardService = moodboardService;
        }
        [BindProperty]
        public Moodboard MoodBoard { get; set; }


        public void OnGet(int Id)
        {
            MoodBoard = _moodboardService.GetMoodboard(Id);
        }
    }
}

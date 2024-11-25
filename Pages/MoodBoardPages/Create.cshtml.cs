using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Pages.MoodBoardPages
{
    public class CreateModel : PageModel
    {
        IMoodboardService _moodboardService;

        public CreateModel(IMoodboardService moodboardService)
        {
            _moodboardService = moodboardService;
        }
        [BindProperty]
        public Moodboard MoodBoard { get; set; }


        public void OnGet(int MBId)
        {
          MoodBoard = _moodboardService.GetMoodboard(MBId);
        }

    }
}

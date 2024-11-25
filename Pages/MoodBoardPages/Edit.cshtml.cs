using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Viber.Models;

namespace Viber.Pages.MoodBoardPages
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Moodboard Moodboard { get; set; }

        public void OnGet()
        {
            var moodboardJson = TempData["MoodboardData"].ToString();
            Moodboard = JsonSerializer.Deserialize<Moodboard>(moodboardJson);
        }
    }
}

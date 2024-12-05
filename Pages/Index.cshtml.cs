using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Viber.Models;

namespace Viber.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        //TODO: RET TILBAG TIL (/"Home")
        public IActionResult OnGet()
        {
            //return RedirectToPage("/Home");
            return RedirectToPage("/MoodBoardPages/ViewMoodBoard", new { Id = 19 });
        }
    }
}

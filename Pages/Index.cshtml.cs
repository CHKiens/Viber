using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Viber.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            //TODO: RET TILBAGE
            //return RedirectToPage("/Home");

            return RedirectToPage("/MoodBoardPages/Edit", new { id = 82 });

        }
    }
}

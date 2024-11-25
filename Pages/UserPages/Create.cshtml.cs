using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Pages.UserPages
{
    public class CreateModel : PageModel
    {
        IUserService _userService;

        [BindProperty]
        public User User { get; set; }
        public CreateModel(IUserService userService)
        {
            _userService = userService;
        }
        
        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _userService.CreateUser(User);
            return RedirectToPage("/Login");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Pages.UserPages
{
    public class CreateModel : PageModel
    {
        private IUserService _userService;

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

            try
            {
                _userService.CreateUser(User);
            }

            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
            }
            
            return RedirectToPage("/Login");
        }
    }
}

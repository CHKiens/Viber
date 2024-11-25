using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Viber.Services.Interfaces;

namespace Viber.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;
        
        [BindProperty]
        public LoginInputModel LoginInput { get; set; }
        
        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }
        
        public class LoginInputModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        
        public void OnGet()
        {
        }
        
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = _userService.AuthenticateUser(LoginInput.Username, LoginInput.Password);
                
                if (user != null)
                {
                    // Authentication successful, set up session or authentication cookie
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    return RedirectToPage("/Home"); // Redirect to a dashboard or home page
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                }
            }

            return Page(); 
        }
    }
}

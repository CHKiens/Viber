using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Pages.UserPages;

public class Delete : PageModel
{
    private IUserService _userService;

    public Delete(IUserService userService)
    {
        _userService = userService;
    }
    
    [BindProperty]
    public User User { get; set; }
    
    
    public void OnGet(int Id)
    {
        User = _userService.GetUser(Id);
    }
    
    public IActionResult OnPost()
    {
        _userService.DeleteUser(User);
        
        return RedirectToPage("/UserPages/Mod");
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Pages.UserPages;

public class Mod : PageModel
{
    IUserService _userService;

    public Mod(IUserService userService)
    {
        _userService = userService;
    }
    
    [BindProperty]
    public List<User> Users { get; set; } = new();
    
    public void OnGet()
    {
        Users = _userService.GetAllUsers();
    }
}
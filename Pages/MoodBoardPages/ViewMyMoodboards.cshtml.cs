using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Pages.MoodBoardPages;

public class ViewMyMoodboards : PageModel
{
    private readonly IMoodboardService _context;
    private readonly IUserService _userService;

    public ViewMyMoodboards(IMoodboardService context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }
    
    public List<Moodboard> Moodboards { get; set; } = new();

    public string Username { get; set; }
    
    public void OnGet(int? id)
    {
        int loggedInUserId = HttpContext.Session.GetInt32("UserId") ?? 0;
        if (id.HasValue)
        {
            Moodboards = _context.GetMoodboardByUserId((int)id);
            Username = _userService.GetUser((int)id).Username;
        }
        else
        {
            Moodboards = _context.GetMoodboardByUserId(loggedInUserId);
            Username = _userService.GetUser(loggedInUserId).Username;
        }


        
    }
    public IActionResult OnPost()
    {
        return Page();
    }
            
}
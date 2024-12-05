using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Pages.MoodBoardPages;

public class ViewMyMoodboards : PageModel
{
    private readonly IMoodboardService _context;

    public ViewMyMoodboards(IMoodboardService context)
    {
        _context = context;
    }
    
    public List<Moodboard> MyMoodboards { get; set; } = new();

    [BindProperty]
    public Moodboard moodboard { get; set; }

    [BindProperty]
    public bool MySwitch { get; set; }
    
    public void OnGet()
    {
        int userIdInt = HttpContext.Session.GetInt32("UserId") ?? 0;
        MyMoodboards = _context.GetMoodboardByUserId(userIdInt);
        
    }
    public IActionResult OnPost()
    {
        _context.DeleteMoodboard(moodboard.MoodboardId);
        return Page();
    }
            
}
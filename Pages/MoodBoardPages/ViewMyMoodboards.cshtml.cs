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
    
    
    public void OnGet()
    {
        string userIdStr = HttpContext.Session.GetString("UserId");
        if (!string.IsNullOrEmpty(userIdStr) && int.TryParse(userIdStr, out int userId))
        {
            MyMoodboards = _context.GetMoodboardByUserId(userId);
        }
    }
}
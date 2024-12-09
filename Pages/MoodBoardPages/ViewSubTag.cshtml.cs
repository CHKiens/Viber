using Microsoft.AspNetCore.Mvc.RazorPages;
using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Pages.MoodBoardPages;

public class ViewSubTag : PageModel
{
    private readonly IMoodboardService _moodboardService;
    private readonly ISubTagService _subTagService;

    public ViewSubTag(IMoodboardService context, ISubTagService subTagService)
    {
        _moodboardService = context;
        _subTagService = subTagService;
    }
    
    public SubTag SubTag { get; set; }
    
    public List<Moodboard> Moodboards { get; set; } = new();
    
    public void OnGet(int Id)
    {
        Moodboards = _moodboardService.GetMoodboardBySubTags(Id);
        SubTag = _subTagService.GetSubTagById(Id);
    }
}
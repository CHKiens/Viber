using Microsoft.AspNetCore.Mvc.RazorPages;
using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Pages.MoodBoardPages;

public class ViewPrimaryTag : PageModel
{
    private readonly IMoodboardService _moodboardService;
    private readonly IPrimaryTagService _primaryTagService;
    private readonly ISubTagService _subTagService;

    public ViewPrimaryTag(IMoodboardService moodboardService, IPrimaryTagService primaryTagService, ISubTagService subTagService)
    {
        _moodboardService = moodboardService;
        _primaryTagService = primaryTagService;
        _subTagService = subTagService;
    }
    
    public PrimaryTag PrimaryTag { get; set; } = new();
    public List<SubTag> SubTags { get; set; } = new();
    
    public Dictionary<int, List<Moodboard>> MoodboardsBySubTag { get; set; } = new();
    
    public void OnGet(int PrimaryTagId)
    {
        this.PrimaryTag = _primaryTagService.GetPrimaryTag(PrimaryTagId);

        SubTags = _subTagService.GetSubTags(PrimaryTagId);

        foreach (var subTag in SubTags)
        {
            var moodboards = _moodboardService.GetMoodboardBySubTags(subTag.SubTagId);
            MoodboardsBySubTag[subTag.SubTagId] = moodboards;
        }
    }
}
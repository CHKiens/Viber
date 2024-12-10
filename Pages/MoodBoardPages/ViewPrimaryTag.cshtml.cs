using Microsoft.AspNetCore.Mvc.RazorPages;
using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Pages.MoodBoardPages;

public class ViewPrimaryTag : PageModel
{
    private readonly IMoodboardService _context;
    private readonly IPrimaryTagService _primaryTagService;
    private readonly ISubTagService _subTagService;

    public ViewPrimaryTag(IMoodboardService context, IPrimaryTagService primaryTagService, ISubTagService subTagService)
    {
        _context = context;
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
            var moodboards = _context.GetMoodboardBySubTags(subTag.SubTagId);
            MoodboardsBySubTag[subTag.SubTagId] = moodboards;
        }

        //foreach (var tag in PrimaryTags)
        //{
        //    var moodboards = _context.GetMoodboardsByPrimaryTagId(tag.PrimaryTagId);
        //    MoodboardsByTag[tag.PrimaryTagId] = moodboards;

        //}
    }
}
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
    
    public List<PrimaryTag> PrimaryTags { get; set; } = new();
    public List<SubTag> SubTags { get; set; } = new();
    
    public Dictionary<int, List<Moodboard>> MoodboardsBySubTag { get; set; } = new();
    
    public void OnGet(int PrimaryTagId)
    {
        SubTags = _subTagService.GetSubTags(PrimaryTagId);
        

        /*foreach (var tag in SubTags)
        {
            // Fetch first 14 moodboards for this tag
            var moodboards = _context.GetMoodboardBySubTags(tag.SubTagId);

            MoodboardsByTag[tag.PrimaryTagId] = moodboards;
                
            var subTags = _subTagService.GetSubTagsByPrimaryTagId(tag.PrimaryTagId);
        }*/
    }
}
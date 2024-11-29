using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Viber.Models;
using Viber.Services.Interfaces;
using Viber.Services.Services;

namespace Viber.Pages
{
    public class HomeModel : PageModel
    {

        private readonly IMoodboardService _context;
        private readonly IPrimaryTagService _primaryTagService;
        private readonly ISubTagService _subTagService;

        public HomeModel(IMoodboardService context, IPrimaryTagService primaryTagService, ISubTagService subTagService)
        {
            _context = context;
            _primaryTagService = primaryTagService;
            _subTagService = subTagService;
        }

        public List<PrimaryTag> PrimaryTags { get; set; }
        public List<SubTag> SubTags { get; set; } = new List<SubTag>();
        public Dictionary<int, List<Moodboard>> MoodboardsByTag { get; set; } = new Dictionary<int, List<Moodboard>>();

        public void OnGet() 
        { 
            PrimaryTags = _primaryTagService.GetPrimaryTags();
            SubTags = _subTagService.GetSubTags();

            foreach (var tag in PrimaryTags)
            {
                // Fetch first 14 moodboards for this tag
                var moodboards = _context.GetMoodboardsByPrimaryTagId(tag.PrimaryTagId);

                MoodboardsByTag[tag.PrimaryTagId] = moodboards;
                
                var subTags = _subTagService.GetSubTagsByPrimaryTagId(tag.PrimaryTagId);
            }
        }
    }
}

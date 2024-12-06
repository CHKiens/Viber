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
        
        
        public Dictionary<int, List<SubTag>> SubTagsByPrimaryTag { get; set; } = new();
        
        public Dictionary<int, List<Moodboard>> MoodboardsByTag { get; set; } = new();

        public void OnGet() 
        { 
            PrimaryTags = _primaryTagService.GetPrimaryTags();

            foreach (var tag in PrimaryTags)
            {
                var moodboards = _context.GetMoodboardsByPrimaryTagId(tag.PrimaryTagId);
                MoodboardsByTag[tag.PrimaryTagId] = moodboards; 
                
                var subtags = _subTagService.GetSubTags(tag.PrimaryTagId);
                SubTagsByPrimaryTag[tag.PrimaryTagId] = subtags;
            }
        }
    }
}

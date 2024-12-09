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

        public string SearchTerm { get; set; }

        public bool IsMoodboard { get; set; } = true;
        
        public List<Moodboard> SearchResultMoodboards { get; set; }
        
        public List <SubTag> SearchResultSubTag { get; set; }
        
        public List<PrimaryTag> PrimaryTags { get; set; }
        
        public Dictionary<int, List<SubTag>> SubTagsByPrimaryTag { get; set; } = new();
        
        public Dictionary<int, List<Moodboard>> MoodboardsByTag { get; set; } = new();

        public IActionResult OnGet(string searchTerm, bool isMoodboard)
        { 
            SearchTerm = searchTerm;
            IsMoodboard = isMoodboard;
            PrimaryTags = _primaryTagService.GetPrimaryTags();

            foreach (var tag in PrimaryTags)
            {
                var moodboards = _context.GetMoodboardsByPrimaryTagId(tag.PrimaryTagId);
                MoodboardsByTag[tag.PrimaryTagId] = moodboards; 
                
                var subtags = _subTagService.GetSubTags(tag.PrimaryTagId);
                SubTagsByPrimaryTag[tag.PrimaryTagId] = subtags;
            }

            if (!string.IsNullOrEmpty(SearchTerm) && IsMoodboard) 
            {
                SearchResultMoodboards = _context.SearchForMoodboards(SearchTerm);
            }

            if (!string.IsNullOrEmpty(searchTerm) && !IsMoodboard)
            {
                SearchResultSubTag = _subTagService.SearchForSubTags(SearchTerm);
            }
            return Page();
        }
    }
}

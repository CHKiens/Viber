using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Pages
{
    public class HomeModel : PageModel
    {

        private readonly IMoodboardService _context;
        private readonly IPrimaryTagService _primaryTagService;

        public HomeModel(IMoodboardService context, IPrimaryTagService primaryTagService)
        {
            _context = context;
            _primaryTagService = primaryTagService;
        }

        public List<PrimaryTag> PrimaryTags { get; set; }
        public Dictionary<int, List<Moodboard>> MoodboardsByTag { get; set; } = new Dictionary<int, List<Moodboard>>();

        public void OnGet() 
        { 
            PrimaryTags = _primaryTagService.GetPrimaryTags();

            foreach (var tag in PrimaryTags)
            {
                // Fetch first 14 moodboards for this tag
                var moodboards = _context.GetMoodboardsByPrimaryTagId(tag.PrimaryTagId);

                MoodboardsByTag[tag.PrimaryTagId] = moodboards;
            }
        }
    }
}

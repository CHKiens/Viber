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
        private readonly IMoodboardService _moodboardService;
        private readonly IPrimaryTagService _primaryTagService;
        private readonly ISubTagService _subTagService;
        private readonly IUserService _userService;

        public HomeModel(IMoodboardService moodboardService, IPrimaryTagService primaryTagService, ISubTagService subTagService, IUserService userService)
        {
            _moodboardService = moodboardService;
            _primaryTagService = primaryTagService;
            _subTagService = subTagService;
            _userService = userService;
        }

        public string SearchTerm { get; set; }

        public bool IsMoodboard { get; set; } = true;

        public User User { get; set; } = new();
        
        public List<Moodboard> SearchResultMoodboards { get; set; } = new();

        public List<SubTag> SearchResultSubTag { get; set; } = new(); 
        
        public List<PrimaryTag> PrimaryTags { get; set; }
        
        public Dictionary<int, List<SubTag>> SubTagsByPrimaryTag { get; set; } = new();
        
        public Dictionary<int, List<Moodboard>> MoodboardsByTag { get; set; } = new();

        public IActionResult OnGet(string searchTerm, bool isMoodboard)
        { 
            SearchTerm = searchTerm;
            IsMoodboard = isMoodboard;
            PrimaryTags = _primaryTagService.GetPrimaryTags();
            int userIdInt = HttpContext.Session.GetInt32("UserId") ?? 0;
            
            User = _userService.GetUser(userIdInt);

            foreach (var tag in PrimaryTags)
            {
                var moodboards = _moodboardService.GetMoodboardsByPrimaryTagId(tag.PrimaryTagId);
                MoodboardsByTag[tag.PrimaryTagId] = moodboards; 
                
                var subtags = _subTagService.GetSubTags(tag.PrimaryTagId);
                SubTagsByPrimaryTag[tag.PrimaryTagId] = subtags;
            }

            if (!string.IsNullOrEmpty(SearchTerm) && IsMoodboard) 
            {
                SearchResultMoodboards = _moodboardService.SearchForMoodboards(SearchTerm);
            }

            if (!string.IsNullOrEmpty(searchTerm) && !IsMoodboard)
            {
                SearchResultSubTag = _subTagService.SearchForSubTags(SearchTerm);
                
                foreach (var tag in SearchResultSubTag)
                {
                    List<Moodboard> mbs = new List<Moodboard>();
                    foreach (var mbst in tag.MoodboardSubTags)
                    {
                        mbs.Add(mbst.Moodboard);
                    }
                    MoodboardsByTag[tag.SubTagId] = mbs.ToList();
                }
                
            }
            return Page();
        }
    }
}

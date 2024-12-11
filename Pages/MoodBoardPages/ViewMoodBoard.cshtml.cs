using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Pages.MoodBoardPages
{
    public class ViewMoodBoardModel : PageModel
    {
        IMoodboardService _moodboardService;
        IUserService _userService;
        public ViewMoodBoardModel(IMoodboardService moodboardService, IUserService userService)
        {
            _moodboardService = moodboardService;
            _userService = userService;
        }
        [BindProperty]
        public Moodboard Moodboard { get; set; }

        [BindProperty] 
        public User User { get; set; } = new();
        
        public User Creator { get; set; }

        [BindProperty]
        public bool Authorized { get; set; }

        [BindProperty]
        public List<ContentContainer> ContentContainers { get; set; }

        public void OnGet(int Id)
        {
            
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            User = _userService.GetUser(userId);
            Moodboard = _moodboardService.GetMoodboardAndCC(Id);
            Creator = _userService.GetUser(Moodboard.UserId);
            Authorized = _userService.CheckUserId(Moodboard.UserId, userId);
            ContentContainers = Moodboard.ContentContainers.Where(cc=>cc.OrderId != null).OrderBy(cc => cc.OrderId).ToList();
        }

        public IActionResult OnPost(int moodboardId)
        {
            this.Moodboard = _moodboardService.GetMoodboard(moodboardId);
            _moodboardService.DeleteMoodboard(Moodboard);
            return RedirectToPage("/MoodboardPages/ViewMyMoodboards");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Pages.MoodBoardPages
{
    public class ViewMoodBoardModel : PageModel
    {
        IMoodboardService _moodboardService;
        public ViewMoodBoardModel(IMoodboardService moodboardService)
        {
            _moodboardService = moodboardService;
        }
        [BindProperty]
        public Moodboard Moodboard { get; set; }

        [BindProperty]
        public List<ContentContainer> ContentContainers { get; set; }

        public void OnGet(int Id)
        {
            Moodboard = _moodboardService.GetMoodboardAndCC(Id);
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

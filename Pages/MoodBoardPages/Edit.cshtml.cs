using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Viber.Models;
using Viber.Services.Interfaces;
using Viber.Services.Services;

namespace Viber.Pages.MoodBoardPages {
    public class EditModel : PageModel {
        IMoodboardService _moodboardService;
        IPrimaryTagService _primaryTagService;
        IContentContainerService _contentContainerService;

        public EditModel(IMoodboardService moodboardService, IPrimaryTagService primaryTagService, IContentContainerService contentContainerService)
        {
            _moodboardService = moodboardService;
            _primaryTagService = primaryTagService;
            _contentContainerService = contentContainerService;
        }
        [BindProperty]
        public Moodboard MoodBoard { get; set; }

        [BindProperty]
        public List<PrimaryTag> PrimaryTags { get; set; }

        [BindProperty]
        public List<int> ContentOrder { get; set; }

        [BindProperty]
        public string BackgroundColor {  get; set; }
        [BindProperty]
        public string TitleColor { get; set; }
        [BindProperty]
        public int id { get; set; }
        public void OnGet(int Id)
        {
            PrimaryTags = _primaryTagService.GetPrimaryTags();
            id = Id;
            MoodBoard = _moodboardService.GetMoodboardAndCC(Id);
            foreach (ContentContainer contentContainer in MoodBoard.ContentContainers)
            {
                contentContainer.OrderId = null;
            }

        }

        public IActionResult OnPost()
        {
            MoodBoard = _moodboardService.GetMoodboard(id);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            MoodBoard.BackgroundColor = BackgroundColor;
            MoodBoard.TitleColor = TitleColor;
            int order = 1;
            foreach (int CCId in ContentOrder)
            {
                // gemmer rækkefølgen
                ContentContainer cc = _contentContainerService.GetContentContainerById(CCId);
                cc.OrderId = order;
                _contentContainerService.EditContainer(cc);
                order++;
            }
            _moodboardService.EditMoodboard(MoodBoard);

            return RedirectToPage("/MoodBoardPages/ViewMoodBoard",  new { MBId = id });
        }



    }
}

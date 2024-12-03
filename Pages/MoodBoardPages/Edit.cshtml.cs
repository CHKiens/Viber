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
        public List<ContentContainer> ContentContainers { get; set; }

        [BindProperty]
        public List<int> ContentOrder { get; set; }


        public void OnGet(int Id)
        {
            PrimaryTags = _primaryTagService.GetPrimaryTags();
            MoodBoard = _moodboardService.GetMoodboardAndCC(Id);
            ContentContainers = MoodBoard.ContentContainers.ToList();
        }

        public IActionResult OnPost()
        {
            int order = 1;
            foreach (int CCId in ContentOrder)
            {
                // gemmer rækkefølgen
                ContentContainer cc = _contentContainerService.GetContentContainerById(CCId);
                cc.OrderId = order;
                _contentContainerService.EditContainer(cc);
                order++;
            }
            return Page();
        }



    }
}

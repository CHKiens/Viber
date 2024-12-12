using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Viber.Models;
using Viber.Services.Interfaces;
using Viber.Services.Services;

namespace Viber.Pages.MoodBoardPages {
    public class EditModel : PageModel {
        IMoodboardService _moodboardService;
        IContentContainerService _contentContainerService;

        public EditModel(IMoodboardService moodboardService, IContentContainerService contentContainerService)
        {
            _moodboardService = moodboardService;
            _contentContainerService = contentContainerService;
        }
        [BindProperty]
        public Moodboard MoodBoard { get; set; }

        [BindProperty]
        public List<int> ContentOrder { get; set; }

        [BindProperty]
        public string BackgroundColor {  get; set; }
        
        [BindProperty]
        public string TitleColor { get; set; }
        
        [BindProperty]
        public string TextboxColor { get; set; }
        
        public void OnGet(int Id)
        {
            MoodBoard = _moodboardService.GetMoodboardAndCC(Id);
            //Setter dem til null, hvis der er sat en order tidligere. Vælges en order ikke til sin contentcontainer vil den v�re null og vil ikke vises
            

        }

        public IActionResult OnPost(int Id)
        {
            _contentContainerService.resetOrder(Id);
            MoodBoard = _moodboardService.GetMoodboard( Id);
            if (BackgroundColor == "#000000")
            {
                BackgroundColor = "#1de0e0";
            }
            
            MoodBoard.BackgroundColor = BackgroundColor;
            MoodBoard.TitleColor = TitleColor;
            
            if (!ModelState.IsValid)
            {
                return Page();
            }
            int order = 1;
            foreach (int CCId in ContentOrder)
            {
                // gemmer r�kkefølgen af hver contentcontainer der er valgt af brugeren
                ContentContainer cc = _contentContainerService.GetContentContainerById(CCId);
                cc.OrderId = order;
                
                if (cc.Type == "text")
                {
                    cc.TextColor = TextboxColor;
                }
                
                _contentContainerService.EditContainer(cc);
                order++;
            }
            _moodboardService.EditMoodboard(MoodBoard);

            return RedirectToPage("/MoodBoardPages/ViewMoodBoard",  new { Id = MoodBoard.MoodboardId });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Viber.Models;

namespace Viber.Pages.MoodBoardPages
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Moodboard Moodboard { get; set; }
        public string test {  get; set; }
        public void OnGet()
        {
            var moodboardJson = TempData["MoodboardData"].ToString();
            
            Moodboard = JsonSerializer.Deserialize<Moodboard>(moodboardJson);
            test = moodboardJson; 
                //Test viser Json indhold, bare en test, skal slettes igen n�r det hele virker.
                //Der bliver sendt s� lidt data herover som muligt. Har derfor ingen direkte reference til User, men i stedet bare et user id
                //Tror �rligt bare at det er nemmest at gemme til DB fra create siden, og derefter hente det her. Det er noget skrammel at sidde og bakse med TempData og JSON
        }
    }
}

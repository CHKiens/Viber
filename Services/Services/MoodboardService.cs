using Microsoft.EntityFrameworkCore;
using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Services.Services {
    public class MoodboardService : IMoodboardService {
        private readonly finsby_dk_db_viberContext _context;

        public MoodboardService(finsby_dk_db_viberContext context)
        {
            _context = context;
        }

        //CRUD
        public void CreateMoodboard(Moodboard moodboard)
        {
            
            _context.Moodboards.Add(moodboard);
            _context.SaveChanges();
            
        }
        public Moodboard GetMoodboard(int moodboardId) 
        {
            return _context.Moodboards.FirstOrDefault(mb => mb.MoodboardId == moodboardId);
        }

        public Moodboard GetMoodboardAndCC(int moodboardId) {
            return _context.Moodboards.Include(mb => mb.ContentContainers).FirstOrDefault(mb => mb.MoodboardId == moodboardId);
                }
        //

        

    }
}

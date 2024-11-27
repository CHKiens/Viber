using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Services.Services {
    public class MoodboardService : IMoodboardService {
        private readonly finsby_dk_db_viberContext _context;

        public MoodboardService(finsby_dk_db_viberContext context)
        {
            _context = context;
        }

        public void CreateMoodboard(Moodboard moodboard)
        {
            moodboard.DateOfCreation = DateTime.Now;
            _context.Moodboards.Add(moodboard);
            _context.SaveChanges();
        }


        public Moodboard GetMoodboard(int moodboardId) 
        {
            return _context.Moodboards.FirstOrDefault(mb => mb.MoodboardId == moodboardId);
        }

        public void UpdateMoodboard(Moodboard moodboard)
        {
            moodboard.UpdateDate = DateTime.Now; 
            _context.Moodboards.Update(moodboard);
            _context.SaveChanges();
        }

        public void UpdateContainerList(Moodboard moodboard)
        {
            
        }
    }
}

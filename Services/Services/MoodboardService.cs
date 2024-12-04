using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Services.Services
{
    public class MoodboardService : IMoodboardService
    {
        private readonly finsby_dk_db_viberContext _context;

        public MoodboardService(finsby_dk_db_viberContext context)
        {
            _context = context;
        }

        //CRUD
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

        public Moodboard GetMoodboardAndCC(int moodboardId) {
            return _context.Moodboards
                .Include(mb => mb.ContentContainers)
                .Include(mb => mb.PrimaryTag)
                .FirstOrDefault(mb => mb.MoodboardId == moodboardId);
                }

        public void EditMoodboard(Moodboard moodboard)
        {
            _context.Update(moodboard);
            _context.SaveChanges();
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

        public List<Moodboard> GetMoodboardsByPrimaryTagId(int primaryTagId, int limit = 14)
        {
            return _context.Moodboards
                .Where(mb => mb.PrimaryTagId == primaryTagId)
                .Take(limit) 
                .ToList();
        }
    }
}

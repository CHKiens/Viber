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

        public Moodboard GetMoodboard(int Id)
        {
            var moodboard = _context.Moodboards.FirstOrDefault(mb => mb.MoodboardId == Id);

            return moodboard;
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

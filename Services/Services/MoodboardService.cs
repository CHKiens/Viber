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

        public Moodboard GetMoodboard(int Id)
        {
            var moodboard = _context.Moodboards.FirstOrDefault(mb => mb.MoodboardId == Id);

            return moodboard;
        }
    }
}

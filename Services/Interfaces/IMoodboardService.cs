using Viber.Models;

namespace Viber.Services.Interfaces {
    public interface IMoodboardService {

        public void CreateMoodboard(Moodboard moodboard);
        public Moodboard GetMoodboard(int moodboardId);
    }
}

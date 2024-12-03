using Viber.Models;

namespace Viber.Services.Interfaces {
    public interface IMoodboardService {

        public void CreateMoodboard(Moodboard moodboard);
        public Moodboard GetMoodboard(int moodboardId);
        public Moodboard GetMoodboardAndCC(int moodboardId);

        public void EditMoodboard(Moodboard moodboard);
    }
}

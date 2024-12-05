using Viber.Models;

namespace Viber.Services.Interfaces {

    public interface IMoodboardService 
    {
        List<Moodboard> GetMoodboardsByPrimaryTagId(int primaryTagId, int limit = 14);

        public void CreateMoodboard(Moodboard moodboard);

        public Moodboard GetMoodboard(int moodboardId);
        public Moodboard GetMoodboardAndCC(int moodboardId);

        public void EditMoodboard(Moodboard moodboard);

        public List<Moodboard> GetMoodboardByUserId(int userId);

        public void UpdateMoodboard(Moodboard moodboard);

        public void UpdateContainerList(Moodboard moodboard);
        public void DeleteMoodboard(int moodboardId);
    }
}

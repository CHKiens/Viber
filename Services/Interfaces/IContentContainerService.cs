using System.ComponentModel;
using Viber.Models;

namespace Viber.Services.Interfaces {
    public interface IContentContainerService {

        public void CreateContainer(ContentContainer container, int moodboardid);

        public void AddContainersToMoodboard(List<string> type, List<string> link, Moodboard moodboard);

        public void DeleteContainer(ContentContainer container);
        public void EditContainer(ContentContainer contentContainer);

        public ContentContainer GetContentContainerById(int id);
        public void resetOrder(int moodboardId);
        public ICollection<ContentContainer> GetContainers(int moodboardid);
    }
}

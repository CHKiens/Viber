using System.ComponentModel;
using Viber.Models;

namespace Viber.Services.Interfaces {
    public interface IContentContainerService {

        public void CreateContainer(ContentContainer container, int moodboardid);

        public void DeleteContainer(ContentContainer container);

        public ICollection<ContentContainer> GetContainers(int moodboardid);
    }
}

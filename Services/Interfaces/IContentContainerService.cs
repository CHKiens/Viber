using Viber.Models;

namespace Viber.Services.Interfaces {
    public interface IContentContainerService {

        public void CreateContainer(ContentContainer container);

        public void DeleteContainer(ContentContainer container);
    }
}

using System.ComponentModel;
using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Services.Services {
    public class ContentContainerService : IContentContainerService {
        private readonly finsby_dk_db_viberContext _context;

        public ContentContainerService(finsby_dk_db_viberContext context)
        {
            _context = context;
        }

        public void CreateContainer(ContentContainer container, int moodboardid)
        {
            container.MoodboardId = moodboardid;
            _context.ContentContainers.Add(container);
        }

        public void DeleteContainer(ContentContainer container)
        {
            _context.ContentContainers.Remove(container);
        }

        public ICollection<ContentContainer> GetContainers(int moodboardid)
        {
            return _context.ContentContainers.Where(c => c.MoodboardId == moodboardid).ToList();
        }
    }
}

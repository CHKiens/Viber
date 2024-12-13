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

        public void AddContainersToMoodboard(List<string> type, List<string> link, Moodboard moodboard)
        {
            for (int i = 0; i < type.Count; i++)
            {
                switch (type[i])
                {
                    case "youtube":
                        link[i] = link[i].Split(".be/")[1];
                        break;
                    case "spotify":
                        link[i] = link[i].Split("com/")[1].Split("si=")[0] + "utm_source=generator";
                        break;
                    default: break;
                }
                var container = new ContentContainer
                {
                    Type = type[i],
                    Link = link[i],
                    MoodboardId = moodboard.MoodboardId

                };
                CreateContainer(container, moodboard.MoodboardId);
                moodboard.ContentContainers.Add(container);
            }
        }


        public void DeleteContainer(ContentContainer container)
        {
            _context.ContentContainers.Remove(container);
        }

        public ContentContainer GetContentContainerById(int id)
        {
            return _context.ContentContainers.FirstOrDefault(CC => CC.ContentContainterId == id);
        }

        public void EditContainer (ContentContainer contentContainer)
        {
            _context.Update(contentContainer);
            _context.SaveChanges();
        }

        //
        public void resetOrder(int moodboardId)
        {
            _context.ContentContainers
                .Where(cc=>cc.MoodboardId == moodboardId)
                .ToList()
                .ForEach(cc => cc.OrderId = null);
            _context.SaveChanges();
        }
        public ICollection<ContentContainer> GetContainers(int moodboardid)
        {
            return _context.ContentContainers.Where(c => c.MoodboardId == moodboardid).ToList();
        }
    }
}

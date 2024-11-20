using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Services.Services {
    public class ContentContainerService : IContentContainerService {
        private readonly finsby_dk_db_viberContext _context;

        public ContentContainerService(finsby_dk_db_viberContext context)
        {
            _context = context;
        }
    }
}

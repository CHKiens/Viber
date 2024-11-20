using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Services.Services {
    public class PrimaryTagService : IPrimaryTagService{
        private readonly finsby_dk_db_viberContext _context;

        public PrimaryTagService(finsby_dk_db_viberContext context)
        {
            _context = context;
        }
    }
}

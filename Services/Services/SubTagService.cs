using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Services.Services {
    public class SubTagService : ISubTagService{
        private readonly finsby_dk_db_viberContext _context;

        public SubTagService(finsby_dk_db_viberContext context)
        {
            _context = context;
        }
    }
}

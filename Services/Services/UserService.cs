using Viber.Models;
using Viber.Services.Interfaces;

namespace Viber.Services.Services {
    public class UserService : IUserService{
        private readonly finsby_dk_db_viberContext _context;

        public UserService (finsby_dk_db_viberContext context)
        {
            _context = context;
        }

        //CRUD

        //Andet
    }
}

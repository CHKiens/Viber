using Viber.Models;

namespace Viber.Services.Interfaces {
    public interface IUserService
    {
        public void CreateUser(User user);

        public User AuthenticateUser(string username, string password);
        /// <summary>
        /// Tjekker om bruger har rettigheder. Ved at se om Xid er lig den bruger der er logget ind's id.
        /// </summary>
        /// <param name="Xid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool CheckUserId(int Xid, int userid);
    }
}

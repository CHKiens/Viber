using Viber.Models;

namespace Viber.Services.Interfaces {
    public interface IUserService
    {
        public void CreateUser(User user);

        public User AuthenticateUser(string username, string password);
    }
}

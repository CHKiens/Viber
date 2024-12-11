using Microsoft.AspNetCore.Identity;
using Viber.Models;
using Viber.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace Viber.Services.Services {
    public class UserService : IUserService{
        private readonly finsby_dk_db_viberContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService (finsby_dk_db_viberContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        //CRUD

        public void CreateUser(User user)
        {
            user.Password = _passwordHasher.HashPassword(user, user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
        
        public User AuthenticateUser(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == username);

            if (user != null)
            {
                var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

                if (result == PasswordVerificationResult.Success)
                {
                    return user; // Authentication successful
                }
            }

            return null; // Authentication failed
        }
        

        //Andet

        public bool CheckUserId (int Xid, int userid)
        {
            bool result = false;
            if (userid == Xid)
            {
                result = true;
            }
            
            return result;
        }

        public User GetUser(int userid)
        {
            return _context.Users.FirstOrDefault(User => User.UserId == userid);
        }
    }
}

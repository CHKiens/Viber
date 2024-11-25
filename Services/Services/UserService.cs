﻿using Microsoft.AspNetCore.Identity;
using Viber.Models;
using Viber.Services.Interfaces;

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
    }
}
using GradeProject.AuthService.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersContext _context;
        private readonly PasswordHasher<User> _hasher;

        public UserRepository(UsersContext context)
        {
            _context = context;
            _hasher = new PasswordHasher<User>();
            //var usr = new User() { Email = "Bob@mail.com", Password = "password", IsActive = true, SubjectId = Guid.NewGuid(), RegisteredAt = DateTime.Now, Username = "Bob" };

        }

        public bool ValidateCredentials(string username, string password)
        {
            // Seed
            //var testUser = new User() { Username = "bob", Email = "bob@mail.com", Password = "password" };
            //var pwd = _hasher.HashPassword(testUser, testUser.Password);
            //testUser.Password = pwd;
            //_context.Users.Add(testUser);
            //_context.SaveChanges();

            var user = FindByEmail(username);
            if (user != null) { return _hasher.VerifyHashedPassword(user, user.Password, password) == PasswordVerificationResult.Success; }

            return false;
        }

        public User FindBySubjectId(string subjectId)
        {
            var subjId = Guid.Parse(subjectId);
            return _context.Users.FirstOrDefault(u => u.SubjectId == subjId);
        }

        public User FindByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public void RegisterUser(User user)
        {
            if (user == null) { throw new ArgumentNullException(nameof(user)); }

            if (_context.Users.FirstOrDefault(u => u.Email == user.Email) != null) { throw new Exception("User with such email is already registered"); }
            //if (FindByUsername(user.Username) != null) { throw new Exception("User with such email is already registered"); }

            var hashedPwd = _hasher.HashPassword(user, user.Password);
            user.Password = hashedPwd;
            user.IsActive = true;

            _context.Add(user);
            _context.SaveChanges();
        }
    }
}

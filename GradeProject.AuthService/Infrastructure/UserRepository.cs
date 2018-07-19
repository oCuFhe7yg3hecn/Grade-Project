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
        }

        public bool ValidateCredentials(string username, string password)
        {
            var user = FindByUsername(username);
            if (user != null) { return _hasher.VerifyHashedPassword(user, user.Password, password) == PasswordVerificationResult.Success; }

            return false;
        }

        public User FindBySubjectId(string subjectId)
        {
            var subjId = Guid.Parse(subjectId);
            return _context.Users.FirstOrDefault(u => u.SubjectId == subjId);
        }

        public User FindByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public void RegisterUser(User user)
        {
            if (user == null) { throw new ArgumentNullException(nameof(user)); }

            if (FindByUsername(user.Email) != null) { throw new Exception("User with such email is already registered"); }
            var hashedPwd = _hasher.HashPassword(user, user.Password);
            user.Password = hashedPwd;
            user.IsActive = true;

            _context.Add(user);
            _context.SaveChanges();
        }
    }
}

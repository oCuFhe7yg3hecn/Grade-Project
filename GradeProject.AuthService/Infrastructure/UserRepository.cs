using Flurl.Http;
using GradeProject.AuthService.Models;
using GradeProject.AuthService.Models.Account;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        public async Task AddAsync(User user)
        {
            if (user == null) { throw new ArgumentNullException(nameof(user)); }

            if (_context.Users.FirstOrDefault(u => u.Email == user.Email) != null) { throw new Exception("User with such email is already registered"); }

            var hashedPwd = _hasher.HashPassword(user, user.Password);
            user.Password = hashedPwd;
            user.IsActive = true;

            await _context.AddAsync(user);
            _context.SaveChanges();
        }
    }
}

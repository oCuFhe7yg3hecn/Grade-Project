using GradeProject.AuthService.Models;
using GradeProject.AuthService.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Infrastructure
{
    public interface IUserRepository
    {
        bool ValidateCredentials(string username, string password);

        User FindBySubjectId(string subjectId);

        User FindByEmail(string email);

        Task AddAsync(User user);
    }
}

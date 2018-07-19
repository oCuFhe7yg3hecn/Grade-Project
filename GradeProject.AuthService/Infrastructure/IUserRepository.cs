using GradeProject.AuthService.Models;
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

        User FindByUsername(string username);

        void RegisterUser(User user);
    }
}

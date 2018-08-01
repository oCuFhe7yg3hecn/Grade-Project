using GradeProject.ScoreService.Domain;
using GradeProject.ScoreService.Infrastructure.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.ScoreService.Controllers
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repo;

        public UserService(IRepository<User> repo)
        {
            _repo = repo;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _repo.WhereAsync(_ => true);
        }
    }
}

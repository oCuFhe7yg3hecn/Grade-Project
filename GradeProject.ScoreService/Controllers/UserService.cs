using GradeProject.ScoreService.Infrastructure.Repos;
using GradeProject.ScoreService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.ScoreService.Controllers
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserScores> _repo;

        public UserService(IRepository<UserScores> repo)
        {
            _repo = repo;
        }

        public async Task<List<UserScores>> GetUsers()
        {
            return await _repo.WhereAsync(_ => true);
        }
    }
}

using GradeProject.ScoreService.Infrastructure.Repos;
using GradeProject.ScoreService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.ScoreService.Services
{
    public class ScoreService
    {
        private readonly IRepository<GameScore> _scoresRepo;
        //private readonly IMapper _mapper;

        public ScoreService(IRepository<GameScore> scoresRepo)
        {
            _scoresRepo = scoresRepo;
        }

        //public List<UserScore> GetUserScores(Guid userId)
        //{
        //    var res = await _scoresRepo.WhereAsync(s => s.UserId == userId);
        //    return res;
        //}
    }
}

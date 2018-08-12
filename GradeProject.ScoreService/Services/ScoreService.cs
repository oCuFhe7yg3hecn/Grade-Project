using AutoMapper;
using GradeProject.ScoreService.Infrastructure.Repos;
using GradeProject.ScoreService.Models;
using GradeProject.ScoreService.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.ScoreService.Services
{
    public class ScoreService : IScoreService
    {
        private readonly IRepository<GameScore> _scoresRepo;
        private readonly IMapper _mapper;

        public ScoreService(IRepository<GameScore> scoresRepo, IMapper mapper)
        {
            _scoresRepo = scoresRepo;
            _mapper = mapper;
        }

        public async Task<UserScores> GetUserScores(Guid userId)
        {
            var userScores = await _scoresRepo.WhereAsync(s => s.UserId == userId);

            var res = new UserScores() { UserId = userId };
            foreach (var score in userScores) { res.Scores.Add(new GameScoreModel(score.Game, score.Score)); }

            return res;
        }


        public async Task AddScore(GameScore score) =>
            await _scoresRepo.AddOneAsync(score);
    }
}

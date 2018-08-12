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
        private readonly IRepository<Score> _scoresRepo;
        private readonly IMapper _mapper;

        public ScoreService(IRepository<Score> scoresRepo, IMapper mapper)
        {
            _scoresRepo = scoresRepo;
            _mapper = mapper;
        }

        public async Task<UserScoresModel> GetUserScores(Guid userId)
        {
            var userScores = await _scoresRepo.WhereAsync(s => s.UserId == userId);

            var res = new UserScoresModel() { UserId = userId };
            foreach (var score in userScores) { res.Scores.Add(new NameScoreModel(score.Game, score.Value)); }

            return res;
        }


        public async Task<GameScoresModel> GetGameScores(string gameName)
        {
            var games = await _scoresRepo.WhereAsync(s => s.Game == gameName);
            var res = new GameScoresModel(gameName);

            foreach (var gameScore in games) { res.Scores.Add(new NameScoreModel(gameScore.UserId.ToString(), gameScore.Value)); }

            return res;
        }

        public async Task AddScore(Score score) =>
            await _scoresRepo.AddOneAsync(score);
    }
}

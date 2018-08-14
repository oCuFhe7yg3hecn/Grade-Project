using AutoMapper;
using GradeProject.ScoreService.Infrastructure.Repos;
using GradeProject.ScoreService.Models;
using GradeProject.ScoreService.Models.DTO;
using MongoDB.Driver;
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

        public async Task<UserScoresModel> GetUserScores(string userId)
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

            foreach (var gameScore in games) { res.Scores.Add(new GameUserScoresModel(gameScore.UserId, gameScore.Value)); }

            return res;
        }

        public async Task AddScore(Score score)
        {
            var userScore = await _scoresRepo.SingleAsync(s => s.UserId == score.UserId && s.Game == score.Game);

            if (userScore == null) { await _scoresRepo.AddOneAsync(score); }
            else
            {
                userScore.Value = score.Value;
                var updateDef = new UpdateDefinitionBuilder<Score>()
                    .Set(s => s.Value, score.Value);

                var res = await _scoresRepo.UpdateOneAsync(s => s.UserId == score.UserId && s.Game == score.Game, updateDef);
            }

        }

    }
}

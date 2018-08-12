using System;
using System.Threading.Tasks;
using GradeProject.ScoreService.Models;
using GradeProject.ScoreService.Models.DTO;

namespace GradeProject.ScoreService.Services
{
    public interface IScoreService
    {
        Task AddScore(Score score);
        Task<UserScoresModel> GetUserScores(Guid userId);
        Task<GameScoresModel> GetGameScores(string gameName);
    }
}
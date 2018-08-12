using System;
using System.Threading.Tasks;
using GradeProject.ScoreService.Models;

namespace GradeProject.ScoreService.Services
{
    public interface IScoreService
    {
        Task<UserScores> GetUserScores(Guid userId);
        Task AddScore(GameScore score);
    }
}
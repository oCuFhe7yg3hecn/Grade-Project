using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.ScoreService.Models.DTO
{
    public class GameUserScoresModel
    {
        public GameUserScoresModel(string userId, float score)
        {
            UserId = userId;
            Score = score;
        }

        public string UserId { get; set; }
        public float Score { get; set; }
    }
}

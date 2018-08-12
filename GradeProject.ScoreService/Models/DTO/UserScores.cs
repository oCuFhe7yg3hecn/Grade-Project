using GradeProject.ScoreService.Infrastructure.Attributes;
using GradeProject.ScoreService.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.ScoreService.Models
{
    public class UserScores
    {
        public UserScores()
        {
            UserId = Guid.NewGuid();
            Scores = new List<GameScoreModel>();
        }

        public Guid UserId { get; set; }
        public List<GameScoreModel> Scores { get; set; }
    }
}

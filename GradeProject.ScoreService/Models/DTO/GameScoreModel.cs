using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.ScoreService.Models.DTO
{
    public class GameScoreModel
    {
        public GameScoreModel(string name, float score)
        {
            Name = name;
            Score = score;
        }

        public string Name { get; set; }
        public float Score { get; set; }
    }
}

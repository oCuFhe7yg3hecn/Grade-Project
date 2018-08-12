using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.ScoreService.Models.DTO
{
    public class GameScoresModel
    {
        public GameScoresModel(string gameName)
        {
            GameName = gameName;
            Scores = new List<NameScoreModel>();
        }

        public string GameName { get; set; }
        public List<NameScoreModel> Scores { get; set; }
    }
}

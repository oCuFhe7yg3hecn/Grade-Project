using GradeProject.ScoreService.Infrastructure.Attributes;
using GradeProject.ScoreService.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.ScoreService.Models
{
    public class UserScoresModel
    {
        public UserScoresModel()
        {
            Scores = new List<NameScoreModel>();
        }

        public string UserId { get; set; }
        public List<NameScoreModel> Scores { get; set; }
    }
}

using GradeProject.ScoreService.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.ScoreService.Models
{
    [CollectionName("Scores")]
    public class GameScore
    {
        public string Game { get; set; }
        public Guid UserId { get; set; }
        public float Score { get; set; }
    }
}

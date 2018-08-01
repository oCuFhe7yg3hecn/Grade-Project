using GradeProject.ScoreService.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.ScoreService.Models
{
    [CollectionName("Scores")]
    public class UserScores
    {
        public Guid UserId { get; set; }
        public Dictionary<string, double> Scores { get; set; }
    }
}

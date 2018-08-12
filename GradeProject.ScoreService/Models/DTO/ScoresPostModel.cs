using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.ScoreService.Models
{
    public class ScoresPostModel
    {
        public Guid UserId { get; set; }
        public int Score { get; set; }
    }
}

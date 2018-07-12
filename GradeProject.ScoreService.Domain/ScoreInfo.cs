using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GradeProject.ScoreService.Domain
{
    public class ScoreInfo
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public string GameName { get; set; }
        public DateTime LastUpdate { get; set; }
        public double Score { get; set; }

        public User User { get; set; }
    }
}

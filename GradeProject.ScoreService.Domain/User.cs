using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GradeProject.ScoreService.Domain
{
    public class User : BaseEntity
    {
        public User()
        {
            StatusId = Guid.NewGuid();

            Status = new List<UserStatus>();
            ScoreInfo = new List<ScoreInfo>();
        }

        public double Total { get; set; }
        public Guid StatusId { get; set; }

        public List<UserStatus> Status { get; set; }
        public List<ScoreInfo> ScoreInfo { get; set; }
    }
}

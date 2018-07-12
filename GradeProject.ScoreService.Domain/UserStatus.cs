using System;
using System.Collections.Generic;
using System.Text;

namespace GradeProject.ScoreService.Domain
{
    public class UserStatus
    {
        public UserStatus()
        {
            UserId = Guid.NewGuid();
            User = new User();

            StatusId = Guid.NewGuid();
            Status = new Status();
        }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid StatusId { get; set; }
        public Status Status { get; set; }
    }
}

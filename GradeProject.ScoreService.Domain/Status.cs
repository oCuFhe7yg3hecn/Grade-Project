using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GradeProject.ScoreService.Domain
{
    public class Status : BaseEntity
    {
        public Status()
        {
            Id = Guid.NewGuid();
            Users = new List<UserStatus>();
        }

        public string Name { get; set; }

        public List<UserStatus> Users { get; set; }
    }
}

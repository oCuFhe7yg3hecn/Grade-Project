using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GradeProject.ScoreService.Domain
{
    public class Status
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<UserStatus> Users { get; set; }
    }
}

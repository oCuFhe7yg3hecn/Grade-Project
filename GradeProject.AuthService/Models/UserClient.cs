using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Models
{
    public class UserClient
    {
        [Key]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Required]
        public Guid ClientId { get; set; }
    }
}

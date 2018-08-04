using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Models.Account
{
    public class ProfileInfo
    {
        public ProfileInfo()
        {
            Id = Guid.NewGuid();
        }
        
        //TODO :Change Type
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string NickName { get; set; }
    }
}

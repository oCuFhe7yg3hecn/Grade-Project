using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Models.Account.Register
{
    public class ProfileRegisterModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        public string Gender { get; set; }

        [Required]
        public IFormFile AvatarImage { get; set; }

        public IFormFile CoverImage { get; set; }

        public string Slogan { get; set; }
    }
}

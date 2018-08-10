using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.ProfileService.Models.DTO
{
    public class UserInsertDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Slogan { get; set; }
        //public IFormFile AvatarImage { get; set; }
        //public IFormFile CoverImage { get; set; }
    }
}

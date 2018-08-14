using GradeProject.AuthService.Infrastructure;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Models.Clients
{
    public class UserClientDTO
    {
        public UserClientDTO()
        {

        }

        public string Type { get; set; }

        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string LogoUri { get; set; }
        public string Secret { get; set; }
    }
}

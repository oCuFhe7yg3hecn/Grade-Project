using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Models.Clients
{
    public class ClientCredentials
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public ClientCredentials(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Models.Clients
{
    public class ClientInsertModel
    {
        public ClientInsertModel()
        {
        }

        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUris { get; set; }
        public string PostLogoutUris { get; set; }
    }
}

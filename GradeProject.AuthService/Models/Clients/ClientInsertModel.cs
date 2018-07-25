﻿using Microsoft.AspNetCore.Http;
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
        public IFormFile ClientImage { get; set; }
        public string ClientImageUrl { get; set; }

        public string ClientUri { get; set; }
        public string RedirectUris { get; set; }
        public string PostLogoutUris { get; set; }

        //public string RequiredScopes { get; set; }
    }
}

using AutoMapper;
using GradeProject.AuthService.Infrastructure.Clients;
using GradeProject.AuthService.Models.Clients;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Infrastructure
{
    public static class ClientMapper
    {
        public static Client MapClient(this IMapper mapper, IClientMapStrategy strategy, ClientInsertModel client) => 
            strategy.Map(client);

    }
}

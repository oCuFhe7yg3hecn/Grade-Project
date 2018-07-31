using GradeProject.AuthService.Models.Clients;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Infrastructure
{
    public interface IClientService
    {
        void AddClient(ClientInsertModel clientDto, Guid userId);
        void GetUserClients(string userId);
    }
}

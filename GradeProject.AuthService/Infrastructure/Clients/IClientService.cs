using GradeProject.AuthService.Models.Clients;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Infrastructure.Clients
{
    public interface IClientService
    {
        void AddClient(ClientInsertModel clientDto, Guid userId);
        Task<List<UserClientDTO>> GetUserClients(Guid userId);
    }
}

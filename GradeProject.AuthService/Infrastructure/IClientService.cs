using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Infrastructure
{
    public interface IClientService
    {
        void AddClient(IdentityServer4.Models.Client newClient);
    }
}

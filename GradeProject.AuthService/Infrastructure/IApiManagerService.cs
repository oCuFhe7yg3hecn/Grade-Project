using GradeProject.AuthService.Models.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Infrastructure
{
    public interface IApiManagmentService
    {
        Task RegisterApi(ClientInsertModel clientData);
    }
}

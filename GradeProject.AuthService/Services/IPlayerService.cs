using GradeProject.AuthService.Models.Account.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Services
{
    public interface IPlayerService
    {
        Task RegisterUserAsync(PlayerRegisteModel userRegModel);
        Task RegisterProfileAsync(PlayerRegisteModel userRegModel);
    }
}

using AutoMapper;
using GradeProject.AuthService.Models;
using GradeProject.AuthService.Models.Account;
using GradeProject.AuthService.Models.Account.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<UserRegisterModel, User>();

            CreateMap<PlayerRegisteModel, User>();

            CreateMap<PlayerRegisteModel, ProfileRegisterModel>();
        }
    }
}

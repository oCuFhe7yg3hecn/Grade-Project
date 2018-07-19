using AutoMapper;
using GradeProject.AuthService.Models;
using GradeProject.AuthService.Models.Account;
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
        }
    }
}

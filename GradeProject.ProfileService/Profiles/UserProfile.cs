using AutoMapper;
using GradeProject.ProfileService.Models;
using GradeProject.ProfileService.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.ProfileService.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserInsertDTO, User>();
        }
    }
}

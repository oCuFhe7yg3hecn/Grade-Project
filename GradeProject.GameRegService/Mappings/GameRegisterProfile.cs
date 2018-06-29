using AutoMapper;
using GradeProject.GameRegService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameRegService.Mappings
{
    public class GameRegisterProfile : Profile
    {
        public GameRegisterProfile()
        {
            CreateMap<RegisterInfo, GameInfo>();
        }
    }
}

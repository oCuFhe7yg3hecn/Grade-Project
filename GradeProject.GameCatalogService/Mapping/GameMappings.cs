using AutoMapper;
using GradeProject.GameCatalogService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Mapping
{
    public class GameMappings : Profile
    {
        public GameMappings()
        {
            CreateMap<GameRegisterModel, GameInfo>();
        }
    }
}

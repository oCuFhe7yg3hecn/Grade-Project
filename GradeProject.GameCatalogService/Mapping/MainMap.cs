using AutoMapper;
using GradeProject.GameCatalogService.Models;
using GradeProject.GameCatalogService.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Mapping
{
    public class MainMap : Profile
    {
        public MainMap()
        {
            CreateMap<GameInfo, GameInfoDTO>()
                .ForMember(dest => dest.ProjectLinks,
                opts => opts.MapFrom(src => src.ProjectLinks.Select(x => new ProjectLink(x.Key, x.Value)).ToList()));
        }
    }
}

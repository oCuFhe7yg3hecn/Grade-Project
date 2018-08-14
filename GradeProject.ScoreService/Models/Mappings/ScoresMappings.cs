using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.ScoreService.Models.Mappings
{
    public class ScoresMappings : AutoMapper.Profile
    {
        public ScoresMappings()
        {
            CreateMap<ScoresPostModel, Score>()
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Score));
        }
    }
}

using AutoMapper;
using GradeProject.AuthService.Models.Clients;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Mappings
{
    public class ClientMappings : Profile
    {
        public ClientMappings()
        {
            CreateMap<ClientInsertModel, Client>()
                .ForMember(dest => dest.RedirectUris, opts => opts.Ignore())
                .ForMember(dest => dest.PostLogoutRedirectUris, opts => opts.Ignore())
                .ForMember(dest => dest.ClientSecrets, opts => opts.ResolveUsing(src => new List<Secret>() { new Secret("secret".Sha256()) }));
                //.ForAllOtherMembers(dest => dest.Ignore());

            //CreateMap<ClientInsertModel, ApiResource>()
            //    .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.ApiName))
            //    .ForMember(dest => dest.ApiSecrets, opts => opts.MapFrom(src => src.ApiSecret));
                //.ForAllOtherMembers(dest => dest.Ignore());
        }
    }
}

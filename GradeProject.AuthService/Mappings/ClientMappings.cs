using AutoMapper;
using GradeProject.AuthService.Infrastructure;
using GradeProject.AuthService.Models.Clients;
using IdentityServer4;
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
                .ForMember(dest => dest.ClientId, opts => opts.MapFrom(src => $"client-{Guid.NewGuid()}"))
                .ForMember(dest => dest.ClientName, opts => opts.MapFrom(src => src.ClientName))
                .ForMember(dest => dest.LogoUri, src => src.Ignore())
                .ForMember(dest => dest.AllowOfflineAccess, src => src.UseValue(true))
                .ForMember(dest => dest.RequireConsent, src => src.UseValue(true))
                .ForMember(dest => dest.LogoUri, opts => opts.MapFrom(src => src.LogoUri))
                .ForMember(dest => dest.RedirectUris, opts => opts.Ignore())
                .ForMember(dest => dest.PostLogoutRedirectUris, opts => opts.Ignore())
                .ForMember(dest => dest.AllowedScopes, opts => opts.UseValue(new List<string>() { IdentityServerConstants.StandardScopes.OpenId,
                                                                                                  IdentityServerConstants.StandardScopes.Profile}));
            CreateMap<Client, UserClientDTO>()
                .ForMember(dest => dest.Type, opts => opts.Ignore())
                .ForMember(dest => dest.Secret, opts => opts.Ignore());

        }
    }
}

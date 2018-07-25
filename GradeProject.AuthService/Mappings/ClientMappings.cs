using AutoMapper;
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
                .ForMember(dest => dest.LogoUri, src => src.Ignore())
                .ForMember(dest => dest.AllowedGrantTypes, src => src.UseValue(GrantTypes.Implicit))
                .ForMember(dest => dest.AllowOfflineAccess, src => src.UseValue(true))
                .ForMember(dest => dest.RequireConsent, src => src.UseValue(true))
                .ForMember(dest => dest.AllowAccessTokensViaBrowser, src => src.UseValue(true))

                .ForMember(dest => dest.RedirectUris, opts => opts.ResolveUsing(src => src.RedirectUris.Split(",")
                                                                                                        .Select(x => x.Trim())
                                                                                                        .ToList()))
                .ForMember(dest => dest.PostLogoutRedirectUris, opts => opts.ResolveUsing(src => src.PostLogoutUris.Split(",")
                                                                                                         .Select(x => x.Trim())
                                                                                                         .ToList()))

                .ForMember(dest => dest.AllowedScopes, opts => opts.UseValue(new List<string>() { IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile }));

            //CreateMap<ClientInsertModel, ApiResource>()
            //    .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.ApiName))
            //    .ForMember(dest => dest.ApiSecrets, opts => opts.MapFrom(src => src.ApiSecret));
            //.ForAllOtherMembers(dest => dest.Ignore());
        }
    }
}

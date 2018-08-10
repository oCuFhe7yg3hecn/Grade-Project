using AutoMapper;
using GradeProject.AuthService.Models;
using GradeProject.AuthService.Models.Account;
using GradeProject.AuthService.Models.Account.Register;
using GradeProject.AuthService.Models.Clients;
using IdentityServer4;
using IdentityServer4.EntityFramework.Entities;
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

            CreateMap<ClientInsertModel, Client>()
                .ForMember(dest => dest.ClientId, opts => opts.UseValue($"client-{Guid.NewGuid()}"))
                .ForMember(dest => dest.ClientName, opts => opts.MapFrom(src => src.ClientName))
                .ForMember(dest => dest.AllowOfflineAccess, src => src.UseValue(true))
                .ForMember(dest => dest.RequireConsent, src => src.UseValue(true))
                .ForMember(dest => dest.LogoUri, opts => opts.MapFrom(src => src.LogoUri ?? "/images/icon.png"))
                .ForMember(dest => dest.RedirectUris, opts => opts.Ignore())
                .ForMember(dest => dest.PostLogoutRedirectUris, opts => opts.Ignore())

                .ForMember(dest => dest.AllowedScopes, opts => opts.UseValue(new List<string>() { IdentityServerConstants.StandardScopes.OpenId,
                                                                                                              IdentityServerConstants.StandardScopes.Profile}));

            CreateMap<Client, UserClientDTO>()
                .ForMember(dest => dest.Type, opts => opts.Ignore())
                .ForMember(dest => dest.Secret, opts => opts.Ignore())
                .ForMember(dest => dest.LogoUri, opts => opts.MapFrom(src => src.LogoUri));
        }
    }
}

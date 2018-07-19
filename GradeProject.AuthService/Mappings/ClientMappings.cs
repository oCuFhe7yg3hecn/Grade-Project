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
        }
    }
}

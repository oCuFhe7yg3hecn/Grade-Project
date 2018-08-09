using AutoMapper;
using GradeProject.AuthService.Models.Clients;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Infrastructure.Clients
{
    public class ImplicitClientMapStrategy : IClientMapStrategy
    {
        private readonly IMapper _mapper;

        public ImplicitClientMapStrategy(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Client Map(ClientInsertModel insertModel)
        {
            var client = _mapper.Map<Client>(insertModel);
            client.AllowedGrantTypes = GrantTypes.Implicit;
            client.RedirectUris = insertModel.RedirectUris.Split(",").Select(x => x.Trim()).ToList();
            client.PostLogoutRedirectUris = insertModel.PostLogoutUris.Split(",").Select(x => x.Trim()).ToList();

            return client;
        }
    }
}

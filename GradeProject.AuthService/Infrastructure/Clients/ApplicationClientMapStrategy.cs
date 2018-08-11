using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GradeProject.AuthService.Models.Clients;
using IdentityServer4.Models;

namespace GradeProject.AuthService.Infrastructure.Clients
{
    public class ApplicationClientMapStrategy : IClientMapStrategy
    {
        private readonly IMapper _mapper;

        public ApplicationClientMapStrategy(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Client Map(ClientInsertModel insertModel)
        {
            var client = _mapper.Map<Client>(insertModel);
            client.AllowedGrantTypes = GrantTypes.ClientCredentials;
            client.ClientSecrets = new List<Secret>() { new Secret($"sercret-{Guid.NewGuid()}".Sha256()) };

            client.AllowedScopes.Add("Platform.ProfileService");
            client.AllowedScopes.Add("Platform.ScoreService");
            client.AllowedScopes.Add("Platform.GameCatalogService");

            client.LogoUri = client.LogoUri ?? "/images/server-app-default.svg";

            return client;
        }
    }
}

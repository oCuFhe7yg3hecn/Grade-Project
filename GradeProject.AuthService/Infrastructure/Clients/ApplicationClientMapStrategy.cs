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
            var client = new Client();
            client.ClientId = $"client-${Guid.NewGuid()}";
            client.ClientName = insertModel.ClientName;
            client.AllowOfflineAccess = true;
            client.AllowedGrantTypes = GrantTypes.ClientCredentials;

            client.AllowedScopes.Add("Platform.ScoreService");
            client.AllowedScopes.Add("Platform.ProfileService");
            client.AllowedScopes.Add("Platform.GameCatalogService");

            client.LogoUri = client.LogoUri ?? "/images/server-app-default.svg";

            return client;
        }
    }
}

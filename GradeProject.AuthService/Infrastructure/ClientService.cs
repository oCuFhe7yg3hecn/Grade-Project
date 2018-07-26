using AutoMapper;
using GradeProject.AuthService.Models.Clients;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Infrastructure
{
    public class ClientService : IClientService
    {
        private readonly ConfigurationDbContext _context;
        private readonly IClientStore _clientStore;
        private readonly IMapper _mapper;

        public ClientService(ConfigurationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddClient(ClientInsertModel clientDto)
        {
            //var client = _mapper.Map<Client>(clientDto);

            var client = new Client() { ClientId = clientDto.ClientId };

            switch (clientDto.Type)
            {
                case "oauth-client":
                    client.AllowedGrantTypes = GrantTypes.Implicit;
                    break;
                case "application":
                    client.AllowedGrantTypes = GrantTypes.ClientCredentials;
                    client.ClientSecrets = new List<Secret>() { new Secret(clientDto.ClientSecret) };
                    client.AllowedScopes = new List<string>() { "Platform.ProfileService" };
                    break;
                default:
                    break;
            }

            //var clientCreds = new ClientCredentials(client.ClientId, client.ClientSecrets.FirstOrDefault().ToString());

            _context.Add(client.ToEntity());
            _context.SaveChanges();
        }


        //newClient.AllowedGrantTypes = GrantTypes.Implicit;
        //newClient.AllowedScopes = new List<string>(){
        //    IdentityServerConstants.StandardScopes.OpenId,
        //    IdentityServerConstants.StandardScopes.Profile,
        //    //"Platform.ProfileService"
        //};
        //newClient.AllowOfflineAccess = true;

        ////var res = IdentityServerConstants.ExternalCookieAuthenticationScheme;
        ////var test = IdentityServerConstants.SignoutScheme;

        //_context.Clients.Add(newClient.ToEntity());
        //_context.SaveChanges();
    }
}

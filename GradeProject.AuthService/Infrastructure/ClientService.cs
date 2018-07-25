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

        public ClientService(ConfigurationDbContext context)
        {
            _context = context;
        }

        public void AddClient(Client client)
        {
          

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

using AutoMapper;
using GradeProject.AuthService.Models;
using GradeProject.AuthService.Models.Clients;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;
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
        private readonly UsersContext _userCtx;
        private readonly IMapper _mapper;

        public ClientService(ConfigurationDbContext context, IMapper mapper, UsersContext userCtx)
        {
            _context = context;
            _mapper = mapper;
            _userCtx = userCtx;
        }

        public void AddClient(ClientInsertModel clientDto, Guid userId)
        {
            var client = _mapper.Map<Client>(clientDto);

            switch (clientDto.Type)
            {
                case "oauth-client":
                    client.AllowedGrantTypes = GrantTypes.Implicit;
                    client.RedirectUris = clientDto.RedirectUris.Split(",").Select(x => x.Trim()).ToList();
                    client.PostLogoutRedirectUris = clientDto.PostLogoutUris.Split(",").Select(x => x.Trim()).ToList();
                    break;
                case "application":
                    client.AllowedGrantTypes = GrantTypes.ClientCredentials;
                    client.ClientSecrets = new List<Secret>() { new Secret(Guid.NewGuid().ToString()) };
                    client.AllowedScopes.Add("Platform.ProfileService");
                    break;
                default:
                    break;
            }

            var user = _userCtx.Users.Include(u => u.Clients).FirstOrDefault(u => u.SubjectId == userId);
            var userClient = new UserClient() { UserId = userId, ClientId = client.ClientId };
            user.Clients.Add(userClient);

            _userCtx.SaveChanges();

            _context.Add(client.ToEntity());
            _context.SaveChanges();
        }

        public void GetUserClients(string userId)
        {

        }
    }
}

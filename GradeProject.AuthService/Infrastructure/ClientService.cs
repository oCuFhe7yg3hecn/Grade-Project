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
    public enum ClientTypes { oauth, application }

    public class ClientService : IClientService
    {
        private readonly ConfigurationDbContext _context;
        private readonly ConfigurationDbContext _confContext;
        private readonly UsersContext _userCtx;
        private readonly IMapper _mapper;

        public ClientService(
            ConfigurationDbContext context,
            ConfigurationDbContext confContext, 
            IMapper mapper, 
            UsersContext userCtx)
        {
            _mapper = mapper;
            _context = context;
            _userCtx = userCtx;
            _confContext = confContext;
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

        public async Task<List<UserClientDTO>> GetUserClients(Guid userId)
        {
            var clients = new List<UserClientDTO>();

            var userClientIds = await _userCtx.UserClient.Where(uc => uc.UserId == userId)
                                                         .Select(uc => uc.ClientId)
                                                         .ToListAsync();
            foreach (var clientId in userClientIds)
            {
                var client = await _confContext.Clients.Include(c => c.AllowedGrantTypes)
                                                       .Include(c => c.ClientSecrets)
                                                       .FirstOrDefaultAsync(c => c.ClientId == clientId);

                var userClient = _mapper.Map<UserClientDTO>(client);

                switch (client.AllowedGrantTypes[0].ToString())
                {
                    case "implicit":
                        userClient.Type = "oauth";
                        break;
                    default:
                        userClient.Type = "application";
                        //userClient.ClientSecret = client.ClientSecrets[0].Value;
                        break;
                }

                clients.Add(userClient);
            }

            return clients;
        }
    }
}

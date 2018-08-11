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

namespace GradeProject.AuthService.Infrastructure.Clients
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
            var client = new Client();

            switch (clientDto.Type)
            {
                case "oauth-client":
                    client = _mapper.MapClient(new ImplicitClientMapStrategy(_mapper), clientDto);
                    break;
                case "application":
                    client = _mapper.MapClient(new ApplicationClientMapStrategy(_mapper), clientDto);
                    break;
                default:
                    break;
            }

            _context.Add(client.ToEntity());

            var user = _userCtx.Users.Include(u => u.Clients).FirstOrDefault(u => u.SubjectId == userId);
            user.Clients.Add(new UserClient()
            {
                UserId = userId,
                ClientId = client.ClientId,
                User = user
            });
            _userCtx.Users.Update(user);

            _userCtx.SaveChanges();
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

                if (client.ClientSecrets.Count > 0)
                {
                    switch (client.AllowedGrantTypes[0].GrantType)
                    {
                        case "implicit":
                            userClient.Type = "oauth";
                            break;
                        default:
                            userClient.Type = "application";
                            userClient.Secret = client.ClientSecrets[0].Value;
                            break;
                    }
                }

                clients.Add(userClient);
            }

            return clients;
        }
    }
}

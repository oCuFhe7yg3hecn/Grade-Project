using System.Linq;
using System.Threading.Tasks;
using GradeProject.AuthService.MongoInfrastructure.Repositories;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.Models;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace GradeProject.AuthService
{
    internal class CustomClientStore : IClientStore
    {
        protected ConfigurationDbContext _context;
        private readonly IMapper _mapper;

        public CustomClientStore(ConfigurationDbContext context)
        {
            _context = context;
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
           var client = await _context.Clients.FirstOrDefaultAsync(c => c.ClientId == clientId);
            return new Client();
        }
    }
}
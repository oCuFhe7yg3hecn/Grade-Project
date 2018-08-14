using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GradeProject.AuthService.Models.Clients;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;

namespace GradeProject.AuthService.Infrastructure
{
    public class ApiManagmentService : IApiManagmentService
    {
        private readonly ConfigurationDbContext _context;
        private readonly IMapper _mapper;

        public ApiManagmentService(ConfigurationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task RegisterApi(ClientInsertModel clientData)
        {
            //var apiResource = new ApiResource(clientData.ApiName);
            ///*{ _mapper.Map<ApiResource>(clientData);*/
            //await _context.ApiResources.AddAsync(apiResource.ToEntity());
            //await _context.SaveChangesAsync();
        }
    }
}

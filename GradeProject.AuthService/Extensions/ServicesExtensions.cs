
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddIdentityService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                // this adds the config data from DB (clients, resources)
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), opts => opts.MigrationsAssembly("GradeProject.AuthService"));
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), opts => opts.MigrationsAssembly("GradeProject.AuthService"));

                    // this enables automatic token cleanup
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                });
                //.AddClientStore<CustomClientStore>();
                //.AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
                //.AddInMemoryClients(IdentityConfig.GetClients());
        }
    }
}

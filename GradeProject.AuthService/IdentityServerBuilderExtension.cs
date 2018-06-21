﻿using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService
{
    public class IdentityServerBuilderExtension
    {
        public static class IdentityServerBuilderExtensions
        {
            public static IIdentityServerBuilder AddMongoRepository(this IIdentityServerBuilder builder)
            {
                builder.Services.AddTransient<IRepository, MongoRepository>();

                return builder;
            }


            /// <summary>
            /// Configure ClientId / Secrets
            /// </summary>
            /// <param name="builder"></param>
            /// <param name="configurationOption"></param>
            /// <returns></returns>
            public static IIdentityServerBuilder AddClients(this IIdentityServerBuilder builder)
            {
                builder.Services.AddTransient<IClientStore, CustomClientStore>();
                builder.Services.AddTransient<ICorsPolicyService, InMemoryCorsPolicyService>();

                return builder;
            }


            /// <summary>
            /// Configure API  &  Resources
            /// Note: Api's have also to be configured for clients as part of allowed scope for a given clientID 
            /// </summary>
            /// <param name="builder"></param>
            /// <returns></returns>
            public static IIdentityServerBuilder AddIdentityApiResources(this IIdentityServerBuilder builder)
            {
                builder.Services.AddTransient<IResourceStore, CustomResourceStore>();

                return builder;
            }

            /// <summary>
            /// Configure Grants
            /// </summary>
            /// <param name="builder">The builder.</param>
            /// <returns></returns>
            public static IIdentityServerBuilder AddPersistedGrants(this IIdentityServerBuilder builder)
            {
                builder.Services.TryAddSingleton<IPersistedGrantStore, CustomPersistedGrantStore>();

                return builder;
            }

        }
    }
}

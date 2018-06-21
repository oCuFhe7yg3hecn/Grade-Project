using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GradeProject.AuthService
{
    public class IdentityConfig
    {
        public const string IdentityHost = "http://localhost:5000";

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("Platform.ProfileService", "ProfileService")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {

                new Client
                {
                    ClientId = "External.WebApp",
                    ClientName = "External WebApp",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    AllowOfflineAccess = true,

                    // === 'true' for external applications so the user have to authorize the external webapp
                    RequireConsent = true,

                    RedirectUris = { "https://localhost:44300/signin-oidc" }, // Url of the WebApp Client
                    PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" }, // Url of the WebApp Client
                    
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "Platform.ProfileService"
                    },
                },

                new Client
                {
                    ClientId = "Platform.AngularClient",
                    ClientName = "Angular Client",
                    //ClientSecrets = { new Secret("angular-secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowOfflineAccess = true,
                    
                    // === 'false' for trusted platform clients so the user will not have to accept the application
                    RequireConsent = false,

                    RedirectUris = { "http://localhost:4200//auth-callback" }, // Url of the WebApp Client
                    PostLogoutRedirectUris = { "http://localhost:4200/" }, // Url of the WebApp Client
                    
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "Platform.ProfileService"
                    },
                },

                new Client
                {
                    ClientId = "Platform.AdminConsoleClient",
                    ClientName = "AdminConsole Client",
                    ClientSecrets = { new Secret("adminconsole-secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    AllowedScopes =
                    {
                        "Platform.TodoApi"
                    },
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "bob",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "Bob"),
                        new Claim("nickname", "User_Bob"),
                        new Claim("website", "https://www.BobAtWeb.com"),
                    }
                }
            };
        }
    }
}

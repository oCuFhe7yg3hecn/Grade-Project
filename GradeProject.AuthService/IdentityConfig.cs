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
                new ApiResource("Platform.ProfileService", "ProfileService"),
                new ApiResource("Platform.GameRegService", "GameRegService")
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
                new Client()
                {
                    ClientId = "postman",
                    ClientName = "Postman Client",
                    ClientSecrets = { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequireConsent = false,
                    AllowAccessTokensViaBrowser = true,

                    AllowOfflineAccess = true,

                    RedirectUris = { "https://www.getpostman.com/oauth2/callback" },
                    PostLogoutRedirectUris = { "https://www.getpostman.com" },
                    EnableLocalLogin = true,

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    },
                },
                new Client()
                {
                    ClientId = "26fad7e9-995c-4b6b-9d16-cc2ca93d19cf",
                    ClientName = "Battleships",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowOfflineAccess = true,

                    RequireConsent = true,

                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { "http://localhost:4200" },
                    PostLogoutRedirectUris = { "http://localhost:4200" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "Platform.ProfileService"
                        //IdentityServerConstants.StandardScopes.OfflineAccess,
                        //"Platform.ProfileService"
                    },
                },

                //new Client
                //{
                //    ClientId = "Platform.MVCClient",
                //    ClientName = "Platform MVC Client",
                //    ClientSecrets = { new Secret("mvc-client-secret".Sha256()) },

                //    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                //    AllowOfflineAccess = true,

                //    RequireConsent = false,

                //    RedirectUris = { "https://localhost:44300/signin-oidc" }, // Url of the WebApp Client
                //    PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" }, // Url of the WebApp Client
                    
                //    AllowedScopes =
                //    {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,
                //        IdentityServerConstants.StandardScopes.OfflineAccess,
                //        "Platform.ProfileService"
                //    },
                //}
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
                },
                 new TestUser
                {
                    SubjectId = "2",
                    Username = "alice",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "Alice"),
                        new Claim("nickname", "User_Alice"),
                    }
                }
            };
        }
    }
}

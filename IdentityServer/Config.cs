using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer
{
    public static class Config
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "f64d05a7-b2b1-4dc5-9aa4-2926fb437853",
                    Username = "test",
                    Password = "123456",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "ST"),
                        new Claim("family_name", "Dockx"),
                        new Claim("role", "Administrator"),
                        new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "ST")
                    }
                },
                new TestUser
                {
                    SubjectId = "c3b7f625-c07f-4d7d-9be1-ddff8ff93b4d",
                    Username = "test2",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Sven"),
                        new Claim("family_name", "Vercauteren"),
                        new Claim("role", "Tour Manager"),
                    }
                }
            };
        }

        public static List<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
               new IdentityResources.OpenId(),
               new IdentityResources.Profile(),
               new IdentityResource("roles", "Your role(s)", new []{"role"}),
            };
        }

        internal static IEnumerable<ApiResource> GetApiResources()
        {
            //Adding claims to this array can add more user info, 
            //this is what will be sent over the wire
            return new[] {
                new ApiResource("testDataGeneratorApi", "Test Data Generator API", new[] {
                    "role",
                    "profile",
                    "given_name",
                    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name",
                    "usertype"
                }),
            };
        }

        public static List<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "Bedording test data generator",
                    ClientId="testDataGeneratorClient",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequireConsent = false,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris =new List<string>
                    {
                        "https://localhost:4200/signin-oidc",
                        "https://localhost:4200/redirect-silentrenew"
                    },
                    AccessTokenLifetime = 180,
                    PostLogoutRedirectUris = new[]{
                        "https://localhost:4200/" },
                    AllowedScopes = new []
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles",
                        "testDataGeneratorApi",
                    }
                }
            };
        }
    }
}

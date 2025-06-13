using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Stock_Trading_System.Config
{
    public static class IdentityServerConfig
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                    new ApiScope("protectedApi", "Protected API Access")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                    new ApiResource("protectedApi", "Protected API")
                    {
                        Scopes = { "protectedApi" }
                    }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                    new Client
                    {
                        ClientId = "test_client",
                        ClientSecrets = { new Secret("test_secret".Sha256()) },
                        AllowedGrantTypes = GrantTypes.ClientCredentials.Concat(GrantTypes.ResourceOwnerPassword).ToList(),
                        AllowedScopes = { "protectedApi", IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile }
                    }
            };
    }
}
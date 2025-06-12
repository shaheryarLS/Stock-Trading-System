using IdentityServer4.Models;

namespace Stock_Trading_System.Config
{
    public static class IdentityServerConfig
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("stockApi", "Stock API"),
                new ApiScope("tradeApi", "Trade API"),
                new ApiScope("userApi", "User API")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("supersecret".Sha256())
                    },

                    AllowedScopes = { "openid", "profile", "stockApi", "tradeApi", "userApi" }
                }
            };
    }
}

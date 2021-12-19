using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace FoodShoop.Services.Identity
{
    public static class SD
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> identityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()

            };

        public static IEnumerable<ApiScope> apiScopes =>
            new List<ApiScope>
            {
                new ApiScope("FoodShop","FoodShop Server"),
                new ApiScope(name: "read",   displayName: "Read your data."),
                new ApiScope(name: "write",  displayName: "Write your data."),
                new ApiScope(name: "delete", displayName: "Delete your data.")
            };

        public static IEnumerable<Client> clients =>
            new List<Client>
            { 
                new Client
                {
                    ClientId="client",
                    ClientSecrets= {new Secret ("secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.ClientCredentials,
                    AllowedScopes={"read","write","profile"}
                },
                new Client
                {
                    ClientId="food",
                    ClientSecrets= {new Secret ("secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.Code,
                    RedirectUris= { "https://localhost:44308/signin-oidc" },
                    PostLogoutRedirectUris={ "https://localhost:44308/signout-callback-oidc" },
                    AllowedScopes=new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "FoodShop"
                    }
                }
            };

    }
}

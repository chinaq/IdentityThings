// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };


        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("api1", "My API")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                // machine to machine client
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },
                
                // interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    
                    // where to redirect to after login
                    // RedirectUris = { "https://localhost:5002/signin-oidc" },
                    RedirectUris = { "http://localhost:5002/signin-oidc" },

                    // where to redirect to after logout
                    // PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    }
                },

                // JavaScript Client
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,

                    // RedirectUris =           { "https://localhost:5003/callback.html" },
                    // PostLogoutRedirectUris = { "https://localhost:5003/index.html" },
                    // AllowedCorsOrigins =     { "https://localhost:5003" },
                    RedirectUris =           { "http://zero.q.q:5003/callback.html", "http://one.q.q:5003/callback.html" },
                    PostLogoutRedirectUris = { "http://zero.q.q:5003/index.html", "http://one.q.q/5003/index.html" },
                    AllowedCorsOrigins =     { "http://zero.q.q:5003", "http://one.q.q:5003" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    }
                },
                new Client
                {
                    ClientId = "the_api_swagger",
                    ClientName = "The API - Swagger",
                    // AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowAccessTokensViaBrowser =  true,
                    RedirectUris =  {"http://zero.q.q:6001/swagger/oauth2-redirect.html", "http://one.q.q:6001/swagger/oauth2-redirect.html"},
                    AllowedScopes =  {"api1"},
                    AllowedCorsOrigins = { "http://zero.q.q:6001", "http://one.q.q:6001" },

                    ClientSecrets = {new Secret("secret".Sha256())}, // change me!
                    RequirePkce = false,
                    RequireClientSecret = false,
                },
            };
    }
}
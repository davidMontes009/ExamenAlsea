using System;
using System.Collections.Generic;
using IdentityServer4.Models;

namespace ExamALSEA.Services
{
    public static class Config
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                new Client
                {
                    ClientId = "cliente1",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secreto".Sha256())
                    },
                    AllowedScopes = { "resourceApi1"}
                }
            };
        }


        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> { new ApiResource("resourceApi1", "My recurso") };
        }

    }
}

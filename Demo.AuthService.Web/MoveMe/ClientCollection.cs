using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Demo.AuthService.Web.MoveMe.Clients;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;

namespace Demo.AuthService.Web.MoveMe
{
    public static class ClientCollection
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new ImplicitClient(DemoConstants.ImplicitClientId, "Implicit Client Demo",
                    new List<string> { DemoConstants.ImplicitClientRedirectUri },  // redirect uris
                    new List<string> { DemoConstants.ImplicitClientPostLogoutUri}, // post logout uris
                    new List<string> {                                             // allowed scopes
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.Email
                    })
            };
        }
    }
}

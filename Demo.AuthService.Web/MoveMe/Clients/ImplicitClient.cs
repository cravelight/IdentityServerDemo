using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;

namespace Demo.AuthService.Web.MoveMe.Clients
{
    public class ImplicitClient : Client
    {
        /// <summary>
        /// A client preconfigured for Implicit flow and JWTs
        /// </summary>
        /// <param name="clientId">The unique ID of the client. This will be referenced by any future client applications in order to interact with the Identity Server.</param>
        /// <param name="clientName">The identifier displayed to the user on any consent screens</param>
        /// <param name="redirectUriList">A collection of URIs that tokens and authorisation codes can be returned to for this client. If a URI is not on this list, then it cannot interact with Identity Server using this client.</param>
        /// <param name="postLogoutRedirectUriList">A collection of URIs that Identity Server can redirect to upon logout. Otherwise the user will stay on the default logout success screen within the Identity Server.</param>
        /// <param name="allowedScopes">AllowedScopes is a list of OpenID Connect scopes allowed to be requested and returned to this client. Not setting this allows all scopes to be retrieved (defaults to an empty list).</param>
        public ImplicitClient(string clientId, string clientName, List<string> redirectUriList, List<string> postLogoutRedirectUriList, List<string> allowedScopes = null)
        {
            ClientId = clientId;
            ClientName = clientName;
            RedirectUris = redirectUriList;
            PostLogoutRedirectUris = postLogoutRedirectUriList;
            AllowAccessToAllScopes = true;
            //AllowedScopes = allowedScopes ?? new List<string>();

            Enabled = true;
            Flow = Flows.Implicit;
            AccessTokenType = AccessTokenType.Jwt;
            RequireConsent = true;
            AllowRememberConsent = true;
        }

    }
}
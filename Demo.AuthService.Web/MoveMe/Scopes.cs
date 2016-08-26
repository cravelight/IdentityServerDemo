﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityServer3.Core.Models;

namespace Demo.AuthService.Web.MoveMe
{
    public static class Scopes
    {
        /// <summary>
        /// Configure the scopes that Identity Server can provide. 
        /// These scopes will display on the OpenID Connect discovery document.
        /// </summary>
        public static IEnumerable<Scope> Get()
        {
            return new List<Scope>
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.Email,
                StandardScopes.Roles,
                StandardScopes.OfflineAccess
            };
        }
    }
}